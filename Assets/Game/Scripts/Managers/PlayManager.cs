using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    public List<UnitDisplay> InGameCard;


    private void Awake()
    {
        instance = this;
    }
}
