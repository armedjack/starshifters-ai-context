using UnityEngine;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{

    /// <summary>
    /// Баф: +к значению всех карт защиты на следующие N ходов.
    /// (Добавится к AddShieldNode)
    /// </summary>
    [CreateNodeMenu("StarShifters/Effects/Buffs/Defense Cards Bonus")]
    public class BuffDefenseNode : EffectNodeBase
    {
        [SerializeField, Min(1)] private int defenseBonus = 1;
        [SerializeField, Min(1)] private int turns = 1;

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }
            context.AddTimedBuff(EffectContext.BuffKey.DefenseCardBonus, defenseBonus, turns);
            Continue(context);
        }
    }
}
