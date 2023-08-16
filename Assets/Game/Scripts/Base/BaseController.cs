using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour , IDestructable
{
    [SerializeField] private Base _base;
    [SerializeField] private Health _health;
    public Health Health => _health;
}
