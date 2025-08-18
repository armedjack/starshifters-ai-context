// ВНИМАНИЕ: фрагмент — замени содержимое цикла в BuildDeckNow()
// Комментарии максимально подробные для начинающего

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StarShifters.Core.Data;                      // CardDef
using StarShifters.Core.Integration.CardHouse;     // CardRuntime, CardHouseCardLink

namespace StarShifters.Core.Integration.CardHouse
{
    public class CardHouseDeckFactory : MonoBehaviour
    {
        [Header("Входные данные")]
        [SerializeField] private DeckPreset deckPreset;

        [Header("CardHouse группы/префабы")]
        [SerializeField] private Transform deckGroup;   // инстанс группы Deck
        [SerializeField] private GameObject cardPrefab; // префаб одной карты с CardHouseCardLink

        [Button(ButtonSizes.Large), GUIColor(0.2f, 0.7f, 0.3f)]
        public void BuildDeckNow()
        {
            if (deckPreset == null || deckGroup == null || cardPrefab == null)
            {
                Debug.LogError("DeckFactory: не заданы DeckPreset/DeckGroup/CardPrefab");
                return;
            }

            // 1) Получаем плоский список карточек из пресета
            List<CardDef> cards = deckPreset.BuildList();

            // Сохраняем ссылку на префаб до очистки, чтобы случайно не уничтожить её,
            // если сам префаб находится внутри deckGroup в сцене.
            var prefab = cardPrefab;

            // 2) Чистим текущие дочерние объекты группы Deck (в редакторе — мгновенно)
            for (int i = deckGroup.childCount - 1; i >= 0; i--)
                DestroyImmediate(deckGroup.GetChild(i).gameObject);

            foreach (var def in cards)
            {
                var go = Instantiate(prefab, deckGroup);

                // 3.1. Находим линк (связка UI <-> Runtime)
                var link = go.GetComponent<CardHouseCardLink>();
                if (link == null)
                {
                    Debug.LogError("На CardPrefab нет CardHouseCardLink — добавь компонент на префаб карты.");
                    DestroyImmediate(go);
                    continue;
                }

                // 3.2. Создаём "чистый" рантайм-объект (НЕ через AddComponent!)
                // ВАЖНО: см. Шаг 2 — у CardRuntime должен быть конструктор, принимающий CardDef.
                var runtime = new CardRuntime(def);

                // 3.3. Биндим рантайм к визуалу (сигнатура линка ожидает CardRuntime)
                link.Bind(runtime);
            }

            Debug.Log($"DeckFactory: собрано {deckGroup.childCount} карт в Deck.");
        }

        [ContextMenu("Build Deck Now (Context Menu)")]
        private void BuildDeckNowContext() => BuildDeckNow();
    }
}
