using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class DeckHandler : MonoBehaviour
{

    public List<UnitDisplay> InGameCard;
    public List<Unit> handDeck = new List<Unit>();
    public List<Unit> reserveDeck = new List<Unit>();
    public UnitDisplay reservedCard;
    [SerializeField] private GameObject _inGameCardPrefab;


    [SerializeField] private List<Transform> _cardHolders;
    [SerializeField] private Transform _reservedCardTransform;

    private void Start()
    {
        SetDeck();
    }


    public void SetDeck()
    {
        ShuffleDeck();
        SetGameStartUnits();
    }

    private void SetGameStartUnits()
    {
        StartCoroutine(SetGameStartCardsIE());
    }

    private IEnumerator SetGameStartCardsIE()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < GameManager.Instance.deck.Count; i++)
        {
            if (i < 4)
            {
                handDeck.Add(GameManager.Instance.deck[i]);
                InGameCard[i].SetUnit(handDeck[i]);
                //PlayManager.instance.InGameCard[i].gameObject.SetActive(true);
            }
            else reserveDeck.Add(GameManager.Instance.deck[i]);
        }
        SetNextReservedCard();
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < GameManager.Instance.deck.Count; i++)
        {
            Unit temp = GameManager.Instance.deck[i];
            int randomIndex = Random.Range(0, GameManager.Instance.deck.Count);
            GameManager.Instance.deck[i] = GameManager.Instance.deck[randomIndex];
            GameManager.Instance.deck[randomIndex] = temp;
        }
    }


    public void OrganiseDeck(Unit unit, GameObject cardGo)
    {
        AddUsedCardToReserve(unit, cardGo);
        RefillHandDeck();
        SpawnReservedCard();
        SetNextReservedCard();
    }

    public void RefillHandDeck()
    {
        for (int i = 0; i < _cardHolders.Count; i++)
        {
            if (_cardHolders[i].childCount == 0)
            {
                reservedCard.gameObject.transform.DOMove(_cardHolders[i].position, 0.25f);
                reservedCard.gameObject.transform.DOScale(Vector3.one, 0.25f);
                reservedCard.GetComponent<GameCardInteraction>().enabled = true;
                reservedCard.transform.SetParent(_cardHolders[i].transform);
                reservedCard.GetComponent<GameCardInteraction>().SetCardTransfrom(_cardHolders[i]);
                Unit tmpUnit = reserveDeck[0];
                reserveDeck.Remove(tmpUnit);
                handDeck.Add(tmpUnit);
                reservedCard = null;
                break;
            }
        }
    }

    private void SpawnReservedCard()
    {
        GameObject tmpReservedCard = Instantiate(_inGameCardPrefab, _reservedCardTransform);
        tmpReservedCard.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0);
        reservedCard = tmpReservedCard.GetComponent<UnitDisplay>();
    }

    private void AddUsedCardToReserve(Unit unit, GameObject cardGo)
    {
        handDeck.Remove(unit);
        reserveDeck.Add(unit);
        DestroyCard(cardGo);
    }


    private void SetNextReservedCard()
    {
        //reservedCard.SetUnit(reserveDeck[0]);
    }

    private void DestroyCard(GameObject cardGo)
    {
        Destroy(cardGo);
    }
}
