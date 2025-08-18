using System;
using System.Collections.Generic;
using UnityEngine;
using StarShifters.Core.Data;
using StarShifters.Core.Runtime;


namespace StarShifters.Core.Integration.CardHouse
{
    /// <summary>
    /// Простейшая реализация колоды/руки/сброса (адаптер под CardHouse).
    /// Позже, когда подключим реальный CardHouse, методы можно перенаправить на его API.
    /// </summary>
    public class PlayerHandController : MonoBehaviour
    {
        [Header("Ссылки")]
        [SerializeField] private TurnController turn; // <<— читаем конфиг отсюда

        [Header("Исходная колода (список CardDef)")]
        public DeckPreset DeckPreset;

        [Header("Локальные настройки (используются ТОЛЬКО как фолбэк)")]
        [Tooltip("Если true — берём значения из TurnController. Если false — используем локальные фолбэки ниже.")]
        [SerializeField] private bool useTurnConfig = true;

        // Эти поля — замена старым DrawPerTurn/HandLimit, но мы переносим их под новые имена
        // и добавляем миграцию, чтобы Unity автоматически перетащил старые значения.
        
        [SerializeField, Min(1)] private int fallbackDrawPerTurn = 4;        
        [SerializeField, Min(1)] private int fallbackHandLimit = 10;

        // ЭФФЕКТИВНЫЕ значения — всегда читаем через свойства:
        public int EffectiveDrawPerTurn =>
            (useTurnConfig && turn != null) ? turn.CardsPerTurn : fallbackDrawPerTurn;

        public int EffectiveHandLimit =>
            (useTurnConfig && turn != null) ? turn.HandLimit : fallbackHandLimit;

        // Рантайм-структуры
        private readonly List<CardRuntime> _drawPile = new();
        private readonly List<CardRuntime> _discardPile = new();
        public readonly List<CardRuntime> Hand = new();

        /// <summary>Событие вызывается при доборе новой карты в руку.</summary>
        public event Action<CardRuntime> OnCardDrawn;

        /// <summary>Инициализация колоды из пресета (в начале новой игры)</summary>
        public void Initialize()
        {
            _drawPile.Clear(); _discardPile.Clear(); Hand.Clear();
            if (DeckPreset != null)
            {
                var defs = DeckPreset.BuildList(); // <-- получаем просто список CardDef
                foreach (var def in defs)
                    _drawPile.Add(new CardRuntime(def)); // <-- оборачиваем в рантайм-объекты
            }

            Debug.Log($"[Hand] Initialize: preset={(DeckPreset != null ? DeckPreset.name : "null")} draw={_drawPile.Count}");
            Shuffle(_drawPile);
        }

        /// <summary>Добор карт. Если драфт-пул пуст — перемешиваем сброс в драфт.</summary>
        public void Draw(int amount)
        {
            Debug.Log($"[Hand] Draw {amount}: before -> draw={_drawPile.Count} discard={_discardPile.Count} hand={Hand.Count}");
            for (int i = 0; i < amount; i++)
            {
                if (_drawPile.Count == 0)
                {
                    // перенос сброса в драфт
                    if (_discardPile.Count == 0)
                    {
                        Debug.Log("[Hand] Draw: both piles empty, stop");
                        return; // нечего тянуть
                    }
                    _drawPile.AddRange(_discardPile);
                    _discardPile.Clear();
                    Shuffle(_drawPile);
                    Debug.Log($"[Hand] Draw: reshuffled discard -> draw={_drawPile.Count}");
                }

                var card = _drawPile[_drawPile.Count - 1];
                _drawPile.RemoveAt(_drawPile.Count - 1);
                Hand.Add(card);
                OnCardDrawn?.Invoke(card);
                Debug.Log($"[Hand] Draw: added '{card.Def.DisplayName}' -> draw={_drawPile.Count} hand={Hand.Count}");
            }
        }

        /// <summary>Сбросить всю руку в сброс.</summary>
        public void DiscardHand()
        {
            _discardPile.AddRange(Hand);
            Hand.Clear();
        }

        /// <summary>Сбросить "лишние" карты сверх лимита руки.</summary>
        public void EnforceHandLimit()
        {
            while (Hand.Count > EffectiveHandLimit)
            {
                var last = Hand[Hand.Count - 1];
                Hand.RemoveAt(Hand.Count - 1);
                _discardPile.Add(last);
            }
        }

        /// <summary>Сбросить конкретную карту (после розыгрыша).</summary>
        public void Discard(CardRuntime card)
        {
            if (Hand.Remove(card))
                _discardPile.Add(card);
        }

        private static void Shuffle(List<CardRuntime> list)
        {
            // простой Fisher–Yates
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
