using UnityEngine;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{
    /// <summary>
    /// Угроза/эффект: штраф к энергии.
    /// Можно снять часть энергии прямо сейчас и/или навесить отрицательный баф на N ходов.
    /// </summary>
    [CreateNodeMenu("StarShifters/Threats/Energy Penalty")]
    public class EnergyPenaltyNode : EffectNodeBase
    {
        [Header("Мгновенный штраф (текущий ход)")]
        [SerializeField, Min(0)] private int currentEnergyPenalty = 0;

        [Header("Штраф на следующие ходы")]
        [SerializeField, Min(0)] private int penaltyPerTurn = 0; // величина штрафа на ход
        [SerializeField, Min(0)] private int turns = 0;          // сколько ходов действует

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }

            // 1) Мгновенный штраф
            if (currentEnergyPenalty > 0)
                context.ApplyEnergyPenaltyNow(currentEnergyPenalty);

            // 2) Отрицательный баф на N ходов
            if (penaltyPerTurn > 0 && turns > 0)
                context.AddTimedBuff(EffectContext.BuffKey.EnergyPerTurn, -penaltyPerTurn, turns);

            Continue(context);
        }
    }
}
