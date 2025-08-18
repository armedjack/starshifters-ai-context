using UnityEngine;
using StarShifters.Core.Runtime;
using StarShifters.Effects;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace StarShifters.Core.Data
{
    /// <summary>
    /// Определение карты. Должен отображаться в Scriptable Sheets.
    /// ВАЖНО: [System.Serializable] нужен, чтобы тип был виден в режиме Assembly Scan.
    /// </summary>
    [System.Serializable] // <-- Добавили это по требованиям Scriptable Sheets (Assembly scan)
    [CreateAssetMenu(fileName = "CardDef", menuName = "StarShifters/Data/Card")]
    public class CardDef : ScriptableObject
    {
#if ODIN_INSPECTOR
        [TitleGroup("Метаданные", Alignment = TitleAlignments.Centered)]
        [HorizontalGroup("Метаданные/Split")]
        [VerticalGroup("Метаданные/Split/Left")]
        [ShowInInspector, HideLabel, DisplayAsString, PropertyOrder(0)]
        private string MetadataLeftPlaceholder => string.Empty;

        [VerticalGroup("Метаданные/Split/Right")]
        [PropertyOrder(1)]
        [LabelText("ID (GUID)")]
        [ReadOnly, ShowInInspector]
#endif
        [SerializeField] private string id;

#if ODIN_INSPECTOR
        [VerticalGroup("Метаданные/Split/Right")]
        [PropertyOrder(3)]
        [LabelText("Название")]
#endif
        public string DisplayName;

#if ODIN_INSPECTOR
        [VerticalGroup("Метаданные/Split/Right")]
        [PropertyOrder(4)]
        [TextArea(2, 4)]
        [LabelText("Описание")]
#endif
        public string Description;

#if ODIN_INSPECTOR
        [TitleGroup("Бой", Alignment = TitleAlignments.Centered)]
#endif
        public CardType Type;

#if ODIN_INSPECTOR
        [Range(1, 3)]
        [LabelText("Стоимость Энергии (1–3)")]
#endif
        public int EnergyCost = 1;

#if ODIN_INSPECTOR        
        [VerticalGroup("Метаданные/Split/Right")]
        [PropertyOrder(2)]
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

            EnergyCost = Mathf.Clamp(EnergyCost, 1, 3);
        }
    }
}
