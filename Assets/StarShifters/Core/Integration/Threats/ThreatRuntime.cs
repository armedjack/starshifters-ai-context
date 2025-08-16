using StarShifters.Core.Runtime;
using StarShifters.Core.Data;
using StarShifters.Effects;

namespace StarShifters.Core.Integration.Threats
{
    /// <summary>
    /// Рантайм-обёртка для угрозы: умеет выполнить её граф.
    /// </summary>
    public class ThreatRuntime
    {
        public readonly ThreatDef Def;
        public ThreatRuntime(ThreatDef def) { Def = def; }

        public string DisplayName => Def.DisplayName;
        public string IconPath => Def.IconPath;
        public EffectGraph Graph => Def.EffectGraph;

        /// <summary>Выполнить эффект угрозы над текущим контекстом.</summary>
        public void Resolve(EffectContext ctx)
        {
            Graph?.Run(ctx);
        }
    }
}
