using UnityEngine;
using CardHouse;
using StarShifters.Core.Integration.CardHouse;

[RequireComponent(typeof(Card))]
public class CardOnPlayBinder : MonoBehaviour
{
    void Awake()
    {
        var bridge = FindFirstObjectByType<CardHouseGameBridge>();  // или FindAnyObjectByType
        if (bridge != null)
        {
            var card = GetComponent<Card>();
            card.OnPlay.AddListener(() => bridge.TryPlayCardGO(card.gameObject));
        }
    }
}
