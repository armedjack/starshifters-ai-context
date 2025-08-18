using UnityEngine;
using CardHouse; // Gate and NoParams

namespace StarShifters.Core.Integration.CardHouse
{
    /// <summary>
    /// Gate that blocks dragging/playing a card when energy is insufficient.
    /// </summary>
    [RequireComponent(typeof(CardHouseCardLink))]
    public class CardEnergyGate : Gate<NoParams>
    {
        [SerializeField] private CardHouseGameBridge gameBridge;
        private CardHouseCardLink link;

        private void Awake()
        {
            link = GetComponent<CardHouseCardLink>();
            if (gameBridge == null)
                gameBridge = FindObjectOfType<CardHouseGameBridge>();
        }

        protected override bool IsUnlockedInternal(NoParams gateParams)
        {
            if (gameBridge == null || link == null || link.Runtime == null)
                return false;
            return gameBridge.CanAfford(link.gameObject);
        }
    }
}
