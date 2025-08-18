using System;
using System.Collections.Generic;
using UnityEngine;

namespace StarShifters.Core.Data
{
    /// <summary>
    /// Пресет колоды: какие карты (CardDef) и в каком количестве.
    /// Это "сырьё" для инициализации рантайм-колоды.
    /// </summary>
    [CreateAssetMenu(fileName = "DeckPreset", menuName = "StarShifters/Data/Deck Preset")]
    public class DeckPreset : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public CardDef Card; // ссылка на определение карты
            [Min(1)] public int Count = 1; // сколько копий положить в колоду
        }

        [Tooltip("Список карт и их количеств. По правилам целевая сумма ≈ 20.")]
        public List<Entry> Entries = new();

        /// <summary>
        /// Вернуть список исходных CardDef с учётом количества.
        /// (Рантайм-обёртки сделаем в контроллере руки.)
        /// </summary>
        public List<CardDef> BuildList()
        {
            var list = new List<CardDef>();
            foreach (var e in Entries)
            {
                if (e.Card == null || e.Count <= 0) continue;
                for (int i = 0; i < e.Count; i++)
                    list.Add(e.Card);
            }
            return list;
        }
    }
}
