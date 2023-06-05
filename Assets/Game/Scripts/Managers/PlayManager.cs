using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    public List<UnitDisplay> InGameCard;
    public List<Unit> handDeck = new List<Unit>();
    public List<Unit> reserveDeck = new List<Unit>();
    public UnitDisplay nextReservedCard;

    private void Awake()
    {
        instance = this;
    }


    private void OnEnable()
    {
        GameEvents.PlayManagerEvents.SpawnUnit += RefillSelectableCards;
        GameEvents.PlayManagerEvents.SpawnUnit += AddUsedCardToReserve;

    }

    private void OnDisable()
    {
        GameEvents.PlayManagerEvents.SpawnUnit -= RefillSelectableCards;
        GameEvents.PlayManagerEvents.SpawnUnit -= AddUsedCardToReserve;
    }

    public void RefillSelectableCards(Unit unit)
    {
        Unit tmpUnit = reserveDeck[0];
        reserveDeck.Remove(tmpUnit);
        handDeck.Add(tmpUnit);
        for (int i = 0; i < InGameCard.Count; i++)
        {
            if (InGameCard[i].Unit == null)
            {
                InGameCard[i].SetUnit(tmpUnit);
                InGameCard[i].gameObject.SetActive(true);
                break;
            }
        }
        SetNextReservedCard();
    }

    private void AddUsedCardToReserve(Unit unit)
    {
        handDeck.Remove(unit);
        reserveDeck.Add(unit);
    }


    private void SetNextReservedCard()
    {
        nextReservedCard.gameObject.SetActive(false);
        nextReservedCard.SetUnit(reserveDeck[0]);
        nextReservedCard.gameObject.SetActive(true);
    }
}
