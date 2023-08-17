using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    [SerializeField] private StaminaHandler _staminaHandler;
    [SerializeField] private DeckHandler _deckHandler;

    public Transform allyBase;
    public Transform enemyBase;
    [SerializeField] private GameObject spawnBoxContainer;
    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 300;
    }

    private void Start()
    {
        _staminaHandler = GetComponent<StaminaHandler>();
        _deckHandler = GetComponent<DeckHandler>();
    }

    public void SwitchSpawnBoxStatus()
    {
        if (spawnBoxContainer.activeInHierarchy)
        {
            spawnBoxContainer.SetActive(false);
        }
        else
        {
            spawnBoxContainer.SetActive(true);
        }
    }
}
