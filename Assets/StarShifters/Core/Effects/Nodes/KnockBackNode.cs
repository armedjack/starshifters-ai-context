using UnityEngine;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{
    /// <summary>
    /// Угроза: отбрасывание назад.
    /// Шаги:
    /// 1) Прямое "сжигание" щита на shieldBurn (если есть щит).
    /// 2) Оставшийся щит поглощает knockDistance (уменьшается на величину поглощения).
    /// 3) Если после 1–2 остался ненулевой knock — увеличиваем дистанцию (бросает назад).
    /// </summary>
    [CreateNodeMenu("StarShifters/Threats/KnockBack")]
    public class KnockBackNode : EffectNodeBase
    {
        [Header("Параметры отбрасывания")]
        [SerializeField, Min(0)] private int shieldBurn = 0;   // прямое сжигание щита ДО поглощения
        [SerializeField, Min(1)] private int knockDistance = 2; // "сила" отбрасывания (в пунктах маршрута)

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }

            // --- Шаг 1. Прямое сжигание щита ---
            if (shieldBurn > 0 && context.Shield > 0)
            {
                // Сжигаем не больше, чем есть
                int burn = Mathf.Min(shieldBurn, context.Shield);
                context.Shield -= burn;
            }

            // --- Шаг 2. Поглощение отбрасывания оставшимся щитом ---
            int remainingKnock = knockDistance;
            if (context.Shield > 0 && remainingKnock > 0)
            {
                // Сколько щит может "поглотить" из нока
                int absorb = Mathf.Min(context.Shield, remainingKnock);
                context.Shield -= absorb;           // щит тратится
                remainingKnock -= absorb;           // остаток нока после поглощения
            }

            // --- Шаг 3. Применяем остаток к дистанции (если он есть) ---
            if (remainingKnock > 0)
            {
                // Увеличиваем дистанцию: отбрасывание назад
                context.AddDistanceDelta(+remainingKnock);
            }

            Continue(context);
        }
    }
}
