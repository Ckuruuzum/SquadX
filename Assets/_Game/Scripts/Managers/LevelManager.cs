using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public Stage[] stages;

    [SerializeField] private STAGERACE _selectedStageRace;
    [SerializeField] private int _humanLevelIndex;
    [SerializeField] private int _demonLevelIndex;
    [SerializeField] private int _zombieLevelIndex;


    [SerializeField] private float _levelProgress;


    public int HumanLevelIndex
    {
        get
        {
            _humanLevelIndex = PlayerPrefs.GetInt("HumanLevelIndex", _humanLevelIndex);
            return _humanLevelIndex;
        }

        set
        {
            _humanLevelIndex = value;


            PlayerPrefs.SetInt("HumanLevelIndex", _humanLevelIndex);
        }
    }
    public int DemonLevelIndex
    {
        get
        {
            _demonLevelIndex = PlayerPrefs.GetInt("DemonLevelIndex", _demonLevelIndex);
            return _demonLevelIndex;
        }

        set
        {
            _demonLevelIndex = value;


            PlayerPrefs.SetInt("DemonLevelIndex", _demonLevelIndex);
        }
    }
    public int ZombieLevelIndex
    {
        get
        {
            _zombieLevelIndex = PlayerPrefs.GetInt("ZombieLevelIndex", _zombieLevelIndex);
            return _zombieLevelIndex;
        }

        set
        {
            _zombieLevelIndex = value;


            PlayerPrefs.SetInt("ZombieLevelIndex", _zombieLevelIndex);
        }
    }
    public float LevelProgress
    {
        get
        {
            _levelProgress = PlayerPrefs.GetFloat("LevelProgress", _levelProgress);
            return _levelProgress;
        }

        set
        {
            _levelProgress = value;
            PlayerPrefs.SetFloat("LevelProgress", _levelProgress);
        }
    }

    public int GetFaction()
    {
        return (int)_selectedStageRace;
    }
    public void SelectStage(int stageIndex)
    {
        _selectedStageRace = (STAGERACE)stageIndex;
        SetStageLevelMap();
    }
    private void SetStageLevelMap()
    {
        switch (_selectedStageRace)
        {
            case STAGERACE.Default:
                break;
            case STAGERACE.Human:
                for (int i = 0; i < stages[1].levelButtons.Length; i++)
                {
                    if (i < HumanLevelIndex)
                    {
                        stages[1].levelButtons[i].interactable = true;
                    }
                    else
                    {
                        stages[1].levelButtons[i].interactable = false;
                    }
                }
                break;
            case STAGERACE.Demon:
                break;
            case STAGERACE.Zombie:
                break;
            default:
                break;
        }
    }
    public void LoadLevel()
    {
        SceneLoader.Instance.LoadScene("Game");
        
        switch (_selectedStageRace)
        {
            case STAGERACE.Default:
                break;
            case STAGERACE.Human:
                Debug.Log(stages[1].level[1].level);
                break;
            case STAGERACE.Demon:
                break;
            case STAGERACE.Zombie:
                break;
            default:
                break;
        }
    }
}

[Serializable]
public enum STAGERACE
{
    Default = 0,
    Human = 1,
    Demon = 2,
    Zombie = 3
}



[Serializable]
public class Stage
{
    public string stageName;
    public Level[] level;
    public Button[] levelButtons;
}

[Serializable]
public class Level
{
    public string level;
    public Unit[] units;
}
