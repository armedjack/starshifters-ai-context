using UnityEngine;
using StarShifters.Core.Runtime;     // наш EffectContext
using StarShifters.Effects;  // наш EffectGraph
using StarShifters.Core.Data;            // CardDef
using StarShifters.Core.Integration.CardHouse; // CardRuntime  


namespace StarShifters.Debugging
{
    /// <summary>
    /// Простой отладчик для запуска графов эффектов в рантайме.
    /// Повесьте на пустой GameObject и назначьте GraphToRun.
    /// </summary>
    [DisallowMultipleComponent]
    public class EffectGraphDebugRunner : MonoBehaviour // <-- ОБЯЗАТЕЛЬНО MonoBehaviour и public
    {
        [Header("Граф для запуска")]
        public EffectGraph GraphToRun;

        [Header("Стартовый контекст")]
        public int StartRemainingDistance = 20;
        public int StartFuel = 10;
        public int StartEnergy = 3;
        public int StartShield = 0;
        [Header("Тест карты (стоимость энергии)")]
        public CardDef TestCard;

        private EffectContext _ctx;
        private string _lastLog = "";

        private void Awake() => ResetContext();

        public void ResetContext()
        {
            _ctx = new EffectContext
            {
                RemainingDistance = StartRemainingDistance,
                Fuel = StartFuel,
                Energy = StartEnergy,
                Shield = StartShield
            };
            _lastLog = $"Контекст: Dist={_ctx.RemainingDistance}, Fuel={_ctx.Fuel}, Energy={_ctx.Energy}, Shield={_ctx.Shield}";
            Debug.Log(_lastLog);
        }

        public void RunGraphOnce()
        {
            if (GraphToRun == null)
            {
                Debug.LogWarning("GraphToRun не назначен");
                return;
            }

            GraphToRun.Run(_ctx);
            _lastLog = $"После Run: Dist={_ctx.RemainingDistance}, Fuel={_ctx.Fuel}, Energy={_ctx.Energy}, Shield={_ctx.Shield}";
            Debug.Log(_lastLog);
        }

        // Примитивный UI для клика мышкой в Play Mode
        private void OnGUI()
        {
            const int pad = 10;
            GUILayout.BeginArea(new Rect(pad, pad, 420, 200), GUI.skin.box);
            GUILayout.Label("EffectGraph Debug Runner");
            GUILayout.Label($"Граф: {(GraphToRun ? GraphToRun.name : "<не назначен>")}");
            GUILayout.Label(_lastLog);
            if (GUILayout.Button("Reset Context")) ResetContext();
            //if (GUILayout.Button("Run Graph Once")) RunGraphOnce();
            

            if (TestCard != null)
            {
                if (GUILayout.Button($"Play Card '{TestCard.DisplayName}' (Cost={TestCard.EnergyCost})"))
                {
                    var runtime = new CardRuntime(TestCard);
                    if (!runtime.TryPlay(_ctx, out var fail))
                        _lastLog = $"Карта не сыграна: {fail}";
                    else
                        _lastLog = $"Карта сыграна → Dist={_ctx.RemainingDistance}, Fuel={_ctx.Fuel}, Energy={_ctx.Energy}, Shield={_ctx.Shield}";
                    Debug.Log(_lastLog);
                }
            }
            else
            {
                GUILayout.Label("Назначьте TestCard для проверки стоимости.");
            }
            GUILayout.EndArea();
        }
    }
}
