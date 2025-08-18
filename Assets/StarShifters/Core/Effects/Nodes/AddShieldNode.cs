using UnityEngine;
using StarShifters.Core.Runtime;
using XNode;

namespace StarShifters.Effects
{
    /// <summary>
    /// Нода эффекта: добавить очки защитного поля на текущий ход.
    /// По правилам щит обнуляется в начале каждого хода.
    /// </summary>
    [CreateNodeMenu("StarShifters/Effects/Add Shield")]
    public class AddShieldNode : EffectNodeBase
    {
        [SerializeField, Min(1)]
        private int shieldAmount = 1;

        [SerializeField, Min(0)]
        private int maxShieldCap = 999;
        // На всякий случай: чтобы не раздувать щит слишком сильно при бафах.

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }
            int amount = shieldAmount;

            // ДОБАВКА от бафов на защиту
            amount += context.GetBuffSum(EffectContext.BuffKey.DefenseCardBonus);
            int newShield = context.Shield + amount;

            if (newShield > maxShieldCap) newShield = maxShieldCap;

            context.Shield = Mathf.Max(0, newShield);

            Continue(context);
        }
    }
}
