using UnityEngine;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{
    /// <summary>
    /// Карточный эффект: отложить срабатывание угроз в конце текущего хода.
    /// Увеличивает счётчик задержек, который проверит контроллер хода.
    /// </summary>
    [CreateNodeMenu("StarShifters/Effects/Delay Threats")]
    public class DelayThreatNode : EffectNodeBase
    {
        [SerializeField, Min(1)] private int charges = 1;

        public override void Execute(EffectContext context)
        {
            if (context == null) { Continue(context); return; }
            context.DelayThreatsCharges += charges;
            Continue(context);
        }
    }
}
