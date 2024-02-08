using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour , IDestructable
{
    public Base baseSO;
    [SerializeField] private BaseHealth _health;
    public TEAM team;
    public BaseHealth Health => _health;

    [Serializable]
    public enum TEAM
    {
        DEFAULT = 0, ALLY = 1, ENEMY = 2
    }
}
