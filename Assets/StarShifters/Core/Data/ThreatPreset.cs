using System;
using System.Collections.Generic;
using UnityEngine;

namespace StarShifters.Core.Data
{
    /// <summary>
    /// Пресет пула угроз: какие ThreatDef и сколько копий каждой.
    /// Заполняется в инспекторе или импортом через Scriptable Sheets.
    /// </summary>
    [CreateAssetMenu(fileName = "ThreatPreset", menuName = "StarShifters/Data/Threat Preset")]
    public class ThreatPreset : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public ThreatDef Threat;   // ссылка на определение угрозы
            [Min(1)] public int Count = 1; // сколько копий в пуле
        }

        [Tooltip("Список угроз и их количества для пула.")]
        public List<Entry> Entries = new();

        /// <summary>Разворачиваем в список ThreatDef с повторами.</summary>
        public List<ThreatDef> BuildList()
        {
            var list = new List<ThreatDef>();
            foreach (var e in Entries)
            {
                if (e.Threat == null || e.Count <= 0) continue;
                for (int i = 0; i < e.Count; i++)
                    list.Add(e.Threat);
            }
            return list;
        }
    }
}
