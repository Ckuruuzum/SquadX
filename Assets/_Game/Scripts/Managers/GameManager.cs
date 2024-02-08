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
        //if (Input.GetKeyDown("space"))
        //{
        //    SetGameStartUnits();
        //}

    }

    private void SetGameSettings()
    {

        //Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }


    
}