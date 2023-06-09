using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    [SerializeField] private StaminaHandler staminaHandler;
    [SerializeField] private DeckHandler deckHandler;


    private void Awake()
    {
        instance = this;
    }
}
