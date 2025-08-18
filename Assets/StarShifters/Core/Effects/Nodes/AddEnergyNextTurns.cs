using UnityEngine;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{
    /// <summary>
    /// Баф: +энергия на следующие N ходов.
    /// </summary>
    [CreateNodeMenu("StarShifters/Effects/Buffs/Add Energy Next Turns")]
    public class AddEnergyNextTurnsNode : EffectNodeBase
    {
        [SerializeField, Min(1)] private int energyPerTurn = 1;
        [SerializeField, Min(1)] private int turns = 1;

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }
            context.AddTimedBuff(EffectContext.BuffKey.EnergyPerTurn, energyPerTurn, turns);
            Continue(context);
        }
    }

}
