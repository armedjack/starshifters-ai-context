using UnityEngine;
using StarShifters.Core.Runtime;
using XNode;

namespace StarShifters.Effects
{
    /// <summary>
    /// Нода эффекта: уменьшить "Оставшуюся дальность полёта" на N.
    /// Это и есть "движение вперёд" по маршруту.
    /// </summary>
    [CreateNodeMenu("StarShifters/Effects/Add Movement")]
    public class AddMovementNode : EffectNodeBase
    {
        [SerializeField, Min(0)]
        private int distanceDelta = 1;
        // Пояснение: distanceDelta задаём положительным числом,
        // а применяем как "минус" к RemainingDistance.

        [SerializeField]
        private bool clampToZero = true;
        // Если true — не дадим "уйти" в отрицательные значения.

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }

            // Снижаем дистанцию: движемся вперёд.
            // базовое значение из ноды
            int amount = distanceDelta;

            // ДОБАВКА от бафов на движение (действуют "на этот ход", если TurnsLeft > 0)
            amount += context.GetBuffSum(EffectContext.BuffKey.MovementCardBonus);


            // Храним знак здесь, чтобы в инспекторе удобно было вводить +2, +4 и т.п.
            int newValue = context.RemainingDistance - amount;

            if (clampToZero && newValue < 0)
                newValue = 0;

            context.RemainingDistance = newValue;

            // Переходим дальше по графу
            Continue(context);
        }
    }
}
