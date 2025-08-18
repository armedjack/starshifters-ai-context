using UnityEngine;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{
   

    /// <summary>
    /// Баф: +к значению всех карт движения на следующие N ходов.
    /// (Добавится к AddMovementNode)
    /// </summary>
    [CreateNodeMenu("StarShifters/Effects/Buffs/Movement Cards Bonus")]
    public class BuffMovementNode : EffectNodeBase
    {
        [SerializeField, Min(1)] private int moveBonus = 1;
        [SerializeField, Min(1)] private int turns = 1;

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }
            context.AddTimedBuff(EffectContext.BuffKey.MovementCardBonus, moveBonus, turns);
            Continue(context);
        }
    }

  
}
