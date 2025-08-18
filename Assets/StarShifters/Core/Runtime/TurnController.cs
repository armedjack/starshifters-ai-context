using UnityEngine;
using StarShifters.Core.Runtime;
using StarShifters.Core.Integration.CardHouse;
using StarShifters.Core.Data;
using StarShifters.Core.Integration.Threats;


namespace StarShifters.Core.Runtime
{
    /// <summary>
    /// Простой контроллер хода по правилам:
    /// 1) Сброс щита -> 2) -1 к топливу -> 3) (здесь будет выкладка угроз)
    /// 4) Добор карт + базовая энергия -> 5) Лимит руки (позже)
    /// 6) Игрок разыгрывает карты -> 7) Конец хода
    /// 8) Сгорает энергия -> 9) Срабатывают угрозы
    /// </summary>
    public class TurnController : MonoBehaviour
    {
        [Header("Параметры партии")]
        [SerializeField, Min(0)] private int StartDistance = 20; // дистанция до цели
        [SerializeField, Min(0)] private int fuel = 10;              // топливо

        [Header("Параметры хода")]
        [SerializeField, Min(1)] private int cardsPerTurn = 4;       // СКОЛЬКО карт добирать в начале хода
        [SerializeField, Min(0)] private int baseEnergyPerTurn = 3;  // СКОЛЬКО энергии давать на ход
        [SerializeField, Min(1)] private int handLimit = 10;         // Лимит руки

        // Публичные геттеры — так удобнее использовать из других классов:
        public int CardsPerTurn => cardsPerTurn;
        public int BaseEnergyPerTurn => baseEnergyPerTurn;
        public int HandLimit => handLimit;

        [Header("Ссылки")]
        public PlayerHandController HandController; // назначить в инспекторе (на том же объекте или другом)
        public ThreatController ThreatController; // ссылка на контроллер угроз
        public CardHouseDeckFactory DeckFactory; // ссылка на фабрику колоды

        // Контекст ресурса/состояния (примерная структура — подгони под свою)
        public EffectContext Context { get; private set; } = new EffectContext();

        // Удобный лог в углу экрана (проверки в прототипе)
        private string _lastLog = "";

        private void Awake()
        {
            StartNewGame();
        }

        /// <summary>
        /// Инициализация новой партии.
        /// </summary>
        public void StartNewGame()
        {
            // Собираем визуальную колоду через CardHouse
            DeckFactory?.BuildDeckNow();

            Context = new EffectContext
            {
                RemainingDistance = StartDistance,
                Fuel = fuel,
                Energy = 0,       // начислим в начале первого хода
                Shield = 0
            };
            if (HandController != null) HandController.Initialize();
            _lastLog = $"Игра старт: Dist={Context.RemainingDistance}, Fuel={Context.Fuel}, Energy={Context.Energy}, Shield={Context.Shield}";
            Debug.Log(_lastLog);

            if (ThreatController != null)
                ThreatController.Initialize();
        }

        /// <summary>
        /// Начало хода: пп.1–4 правил.
        /// </summary>
        public void StartTurn()
        {
            // 1) Сброс щита
            Context.Shield = 0;

            // 2) Топливо -1
            Context.Fuel = Mathf.Max(0, Context.Fuel - 1);

            // 3) (выкладка угроз 1–3 из пула)
            if (ThreatController != null)
                ThreatController.SpawnThreatsForTurn();

            // 4)  Добор карт и энергия
            //    - базовая энергия по правилам
            //    - + энергия от активных бафов (через OnStartTurn)
            if (HandController != null)
            {
                int beforeHand = HandController.Hand.Count;
                HandController.Draw(CardsPerTurn);
                Debug.Log($"[Turn] Draw requested {CardsPerTurn}: hand {beforeHand} -> {HandController.Hand.Count}");
            }

            Context.AddEnergy(BaseEnergyPerTurn);
            Context.OnStartTurn(); // применит EnergyPerTurn бафы и тикнёт их длительность

            // 5) Лимит руки
            if (HandController != null)
            {
                HandController.EnforceHandLimit();
                Debug.Log($"[Turn] After limit: hand={HandController.Hand.Count}");
            }

            _lastLog = $"Начало хода → Dist={Context.RemainingDistance}, Fuel={Context.Fuel}, Energy={Context.Energy}, Shield={Context.Shield}";
            Debug.Log(_lastLog);
        }

        /// <summary>
        /// Попытаться сыграть карту (CardDef) в текущем ходу.
        /// </summary>
        public bool TryPlayCard(CardRuntime card)
        {
            if (card == null) { _lastLog = "Карта не выбрана."; return false; }
                        
            if (!card.TryPlay(Context, out var fail)) // играем карту (метод TryPlay в CardRuntime) и проверяем если фейл
            {
                _lastLog = $"НЕ сыграли '{card.Def.DisplayName}': {fail}";
                Debug.LogWarning(_lastLog);
                return false;
            }

            // После розыгрыша карта идёт в сброс
            HandController?.Discard(card);

            _lastLog = $"Сыграли '{card.Def.DisplayName}' (Cost={card.Def.EnergyCost}) → Dist={Context.RemainingDistance}, Energy={Context.Energy}, Shield={Context.Shield}";
            Debug.Log(_lastLog);
            return true;
        }

        /// <summary>
        /// Конец хода: пп.7–9 правил.
        /// </summary>
        public void EndTurn()
        {
            // 7) Нажатие "конец хода" — здесь логика сдачи хода

            // 8) Сгорание энергии (если нет спец-модуля на перенос)
            Context.Energy = 0;

            // 8.1) сброс всей руки
            HandController?.DiscardHand();

            // 9) Срабатывание угроз (будет через их EffectGraph.Run)
            if (Context.DelayThreatsCharges > 0)
            {
                Context.DelayThreatsCharges--;
                ThreatController?.SkipResolveThisTurn(); // оставляем угрозы на столе
                _lastLog = $"Угрозы отложены. Осталось задержек: {Context.DelayThreatsCharges}.";
            }
            else
            {
                ThreatController?.ResolveActiveThreats(Context); // применяем графы угроз
                _lastLog = "Угрозы сработали.";
            }
            Debug.Log(_lastLog);
        }

        /*private void OnGUI()
        {
            // Панель статуса
            const int pad = 10;
            GUILayout.BeginArea(new Rect(Screen.width - 380 - pad, pad, 380, 320), GUI.skin.box);
            GUILayout.Label("<b>Turn Controller</b>");
            GUILayout.Label($"Dist={Context.RemainingDistance}  Fuel={Context.Fuel}  Energy={Context.Energy}  Shield={Context.Shield}");
            GUILayout.Space(6);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start New Game")) StartNewGame();
            if (GUILayout.Button("Start Turn (1–5)")) StartTurn();
            if (GUILayout.Button("End Turn (7–9)")) EndTurn();
            GUILayout.EndHorizontal();

            GUILayout.Space(6);
            // Рисуем руку с кнопками «сыграть»
            if (HandController != null)
            {
                GUILayout.Label($"Рука: {HandController.Hand.Count} карт");
                var hand = HandController.Hand;
                for (int i = 0; i < hand.Count; i++)
                {
                    var c = hand[i];
                    if (GUILayout.Button($"{c.Def.DisplayName}  [Cost {c.Def.EnergyCost}]  ({c.Def.Type})"))
                        TryPlayCard(c);
                }
            }
            if (ThreatController != null)
            {
                var list = ThreatController.ActiveBoard;
                GUILayout.Label($"Угрозы на столе: {list.Count} (Draw:{ThreatController.DrawCount} / Discard:{ThreatController.DiscardCount})");
                for (int i = 0; i < list.Count; i++)
                {
                    GUILayout.Label($"• {list[i].DisplayName}");
                }
            }

            GUILayout.Space(6);
            GUILayout.Label(_lastLog, GUILayout.Height(100));
            GUILayout.EndArea();
        }*/
    }
}
