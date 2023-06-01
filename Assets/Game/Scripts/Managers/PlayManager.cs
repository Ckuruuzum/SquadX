using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    public List<UnitDisplay> InGameCard;
    public List<Unit> selectableSquadUnits = new List<Unit>();
    public List<Unit> reserveSquadUnits = new List<Unit>();

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameEvents.PlayManagerEvents.RefillSelectable += RefillSelectable;
    }

    private void OnDisable()
    {
        GameEvents.PlayManagerEvents.RefillSelectable -= RefillSelectable;
    }

    public void RefillSelectable()
    {
        Unit tmpUnit = reserveSquadUnits[0];
        reserveSquadUnits.Remove(tmpUnit);
        selectableSquadUnits.Add(tmpUnit);
        for (int i = 0; i < InGameCard.Count; i++)
        {
            if (InGameCard[i].unit == null)
            {
                InGameCard[i].unit = tmpUnit;
                InGameCard[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}
