// CardHouseGameBridge.cs
// Назначение: мост между игровой логикой и CardHouse-операторами.
// Вызывается из UI (Start/End Turn, Play), а в ответ триггерит CardHouse Deal/Discard.
// Пиши в папку: Assets/StarShifters/Core/Integration/CardHouse/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StarShifters.Core.Data;               // DeckPreset/CardDef
using StarShifters.Core.Runtime;            // TurnController/EffectContext (по структуре v0.0.3)
using CardHouse;                            // CardGroup/Card

namespace StarShifters.Core.Integration.CardHouse
{
    [Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }

    public class CardHouseGameBridge : MonoBehaviour
    {
        [Header("Ссылки")]
        [SerializeField] private TurnController turn;

        [Header("CardHouse Operators (назначаются в инспекторе)")]
        // Оператор перемещения КОНКРЕТНОЙ карты (GO) из Deck в Hand
        [SerializeField] private GameObjectEvent OnRequestDealToHand;
        // Оператор перемещения КОНКРЕТНОЙ карты (GO) в Discard
        [SerializeField] private GameObjectEvent OnRequestDiscard;

        [Header("CardHouse Groups (опционально)")]
        [SerializeField] private CardGroup handGroup; // фолбэк, если оператор не назначен

        [Header("Префабы")]
        [SerializeField] private GameObject cardPrefab;

        public TurnController Turn => turn;

        private void Awake()
        {
            if (handGroup == null)
                handGroup = GroupRegistry.Instance?.Get(GroupName.Hand, PhaseManager.Instance?.PlayerIndex);
        }
        
        // Вызывается кнопкой "Start Turn" (или твоим UI)
        public void OnStartTurn()
        {
            if (turn == null) { Debug.LogError("TurnController не назначен"); return; }

            Debug.Log("[Bridge] OnStartTurn pressed");
            turn.StartTurn(); // логика: shield=0, fuel-1, угрозы 1..3, энергия +3, лимит руки 10, и т.д.
        }

        // Вызывается кнопкой "End Turn"
        public void OnEndTurn()
        {
            if (turn == null) return;

            // Перенести все визуальные карты из руки в сброс перед завершением хода
            if (handGroup != null && OnRequestDiscard != null)
            {
                var cards = new List<Card>(handGroup.MountedCards);
                foreach (var card in cards)
                {
                    OnRequestDiscard.Invoke(card.gameObject);
                }
            }

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
            // после успешного розыгрыша убираем карту из руки и отправляем визуал в Discard
            if (link.Runtime.TryPlay(turn.Context, out var failReason))
            {
                // удалить из руки в логике
                turn.HandController?.Discard(link.Runtime);

                // визуально переместить в Discard
                OnRequestDiscard?.Invoke(cardGO);
            }
            else
            {
                Debug.LogWarning(failReason);
            }
        }

        private void OnEnable()
        {
            if (turn != null && turn.HandController != null)
                turn.HandController.OnCardDrawn += HandleCardDrawn;
        }

        private void OnDisable()
        {
            if (turn != null && turn.HandController != null)
                turn.HandController.OnCardDrawn -= HandleCardDrawn;
        }

        private void HandleCardDrawn(CardRuntime runtime)
        {
            if (cardPrefab == null)
            {
                Debug.LogError("[Bridge] Card prefab is not assigned");
                return;
            }

            var go = Instantiate(cardPrefab);
            var link = go.GetComponent<CardHouseCardLink>();
            if (link != null)
                link.Bind(runtime);
            else
                Debug.LogError("[Bridge] Card prefab missing CardHouseCardLink");

            var cardComponent = go.GetComponent<Card>();
            if (cardComponent == null)
            {
                Debug.LogError("[Bridge] Card prefab missing Card component");
                return;
            }

            bool dealtViaOperator = false;
            if (OnRequestDealToHand != null && OnRequestDealToHand.GetPersistentEventCount() > 0)
            {
                var previousGroup = cardComponent.Group;
                OnRequestDealToHand.Invoke(go);
                if (cardComponent.Group != previousGroup)
                    dealtViaOperator = true;
            }

            if (!dealtViaOperator)
            {
                if (handGroup != null)
                    handGroup.Mount(cardComponent, instaFlip: true);
                else
                    Debug.LogWarning("[Bridge] No hand group available to mount card");
            }
        }
    }
}
