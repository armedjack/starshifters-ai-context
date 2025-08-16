// CardHouseGameBridge.cs
// Назначение: мост между игровой логикой и CardHouse-операторами.
// Вызывается из UI (Start/End Turn, Play), а в ответ триггерит CardHouse Deal/Discard.
// Пиши в папку: Assets/StarShifters/Core/Integration/CardHouse/

using System;
using UnityEngine;
using UnityEngine.Events;
using StarShifters.Core.Data;               // DeckPreset/CardDef
using StarShifters.Core.Runtime;            // TurnController/EffectContext (по структуре v0.0.3)

namespace StarShifters.Core.Integration.CardHouse
{
    [Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }

    public class CardHouseGameBridge : MonoBehaviour
    {
        [Header("Ссылки")]
        [SerializeField] private TurnController turn;

        [Header("CardHouse Operators (назначаются в инспекторе)")]
        // Оператор раздачи 1 карты из Deck в Hand (вызовем 4 раза на старте хода)
        [SerializeField] private UnityEvent OnRequestDealToHand;
        // Оператор перемещения КОНКРЕТНОЙ карты (GO) в Discard
        [SerializeField] private GameObjectEvent OnRequestDiscard;

        public TurnController Turn => turn;
        
        // Вызывается кнопкой "Start Turn" (или твоим UI)
        public void OnStartTurn()
        {
            if (turn == null) { Debug.LogError("TurnController не назначен"); return; }

            turn.StartTurn(); // логика: shield=0, fuel-1, угрозы 1..3, энергия +3, лимит руки 10, и т.д.

            // Визуальная раздача 4 карт в руку (по правилам добор=4)
            for (int i = 0; i < turn.CardsPerTurn; i++)
                OnRequestDealToHand?.Invoke();
        }

        // Вызывается кнопкой "End Turn"
        public void OnEndTurn()
        {
            if (turn == null) return;
            turn.EndTurn(); // логика: энергия=0, рука в сброс, угрозы срабатывают (если не отложены)
        }

        // Проверка: хватает ли энергии на карту (для Gate и подсветки)
        public bool CanAfford(GameObject cardGO)
        {
            if (turn == null || cardGO == null) return false;
            var link = cardGO.GetComponent<CardHouseCardLink>();
            if (link == null || link.Runtime == null || link.Runtime.Def == null) return false;

            int cost = Mathf.Max(0, link.Runtime.Def.EnergyCost);
            return turn.Context.Energy >= cost;
        }

        // Попытка сыграть карту по клику/дропу
        public void TryPlayCardGO(GameObject cardGO)
        {
            
            if (turn == null || cardGO == null) return;
            var link = cardGO.GetComponent<CardHouseCardLink>();
            if (link == null || link.Runtime == null) return;

            // 1) Проверяем энергию
            if (!CanAfford(cardGO))
            {
                Debug.Log("Не хватает энергии для розыгрыша карты.");
                return;
            }

            // 2) Разыгрываем карту (списывает энергию и запускает граф эффектов)
            // В v0.0.3 это делает CardRuntime.TryPlay(...) ИЛИ turn.TryPlayCard(...)
            // Оставляем безопасный вызов через рантайм:


            if (link.Runtime.TryPlay(turn.Context, out var failReason))
            {
                OnRequestDiscard?.Invoke(cardGO);
            }
        }
    }
}
