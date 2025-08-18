using UnityEngine;
using StarShifters.Core.Runtime;
using StarShifters.Effects;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace StarShifters.Core.Data
{
    /// <summary>
    /// Определение угрозы. Также должен показываться/импортироваться.
    /// </summary>
    [System.Serializable] // <-- Обязателен для Assembly scan по доке
    [CreateAssetMenu(fileName = "ThreatDef", menuName = "StarShifters/Data/Threat")]
    public class ThreatDef : ScriptableObject
    {
#if ODIN_INSPECTOR
        [VerticalGroup("Метаданные/Split/Right")]
        [ReadOnly, ShowInInspector, LabelText("ID (GUID)")]
#endif
        [SerializeField] private string id;

#if ODIN_INSPECTOR
        [VerticalGroup("Метаданные/Split/Right")]
        [PropertyOrder(2)]
        [LabelText("Название")]
#endif
        public string DisplayName;

#if ODIN_INSPECTOR
        [VerticalGroup("Метаданные/Split/Right")]
        [PropertyOrder(3)]
        [TextArea(2, 4)]
        [LabelText("Описание")]
#endif
        public string Description;

        public ThreatType ThreatType = ThreatType.Generic;


#if ODIN_INSPECTOR
        [TitleGroup("Метаданные", Alignment = TitleAlignments.Centered)]
        [HorizontalGroup("Метаданные/Split", 60, LabelWidth = 80)]
        [LabelText("Icon Path")]
#endif
        public string IconPath;

#if ODIN_INSPECTOR
        [TitleGroup("Эффекты", Alignment = TitleAlignments.Centered)]
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        [LabelText("Граф Эффектов (xNode)")]
#endif

        public EffectGraph EffectGraph;

        public string Id => id;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
                id = System.Guid.NewGuid().ToString();
        }
    }
}
