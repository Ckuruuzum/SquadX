using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        GameEvents.PlayManagerEvents.SpawnUnit += OrgoniseDeck;
    }

    private void OnDisable()
    {
        GameEvents.PlayManagerEvents.SpawnUnit -= OrgoniseDeck;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetNextReservedCard();
        }
    }

    private void OrgoniseDeck(Unit unit, PointerEventData eventData)
    {
        AddUsedCardToReserve(unit);
        RefillHandDeck(eventData);
        SetNextReservedCard();
    }

    public void RefillHandDeck(PointerEventData eventData)
    {
        Unit tmpUnit = reserveDeck[0];
        reserveDeck.Remove(tmpUnit);
        handDeck.Add(tmpUnit);
        eventData.selectedObject.GetComponent<UnitDisplay>().SetUnit(tmpUnit);
    }

    private void AddUsedCardToReserve(Unit unit)
    {
        handDeck.Remove(unit);
        reserveDeck.Add(unit);
    }


    private void SetNextReservedCard()
    {
        nextReservedCard.SetUnit(reserveDeck[0]);
    }
}
