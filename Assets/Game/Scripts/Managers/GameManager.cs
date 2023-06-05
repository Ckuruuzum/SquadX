using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    public List<Unit> deck = new List<Unit>();




    protected override void Awake()
    {
        base.Awake();
        SetGameSettings();
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetGameStartUnits();
        }

    }

    private void SetGameSettings()
    {

        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }


    public void SetGameStart()
    {
        ShuffleSquad();
        SetGameStartUnits();
    }

    private void SetGameStartUnits()
    {
        StartCoroutine(SetGameStartCardsIE());
    }

    private IEnumerator SetGameStartCardsIE()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < deck.Count; i++)
        {
            if (i < 4)
            {
                PlayManager.instance.handDeck.Add(deck[i]);
                PlayManager.instance.InGameCard[i].SetUnit(PlayManager.instance.handDeck[i]);
                PlayManager.instance.InGameCard[i].gameObject.SetActive(true);
            }
            else PlayManager.instance.reserveDeck.Add(deck[i]);
        }
        SetNextReservedCard();
    }

    private void ShuffleSquad()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Unit temp = deck[i];
            int randomIndex = Random.Range(0, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private void SetNextReservedCard()
    {
        PlayManager.instance.nextReservedCard.SetUnit(PlayManager.instance.reserveDeck[0]);
        PlayManager.instance.nextReservedCard.gameObject.SetActive(true);
    }
}