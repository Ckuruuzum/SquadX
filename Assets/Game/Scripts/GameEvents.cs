using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<Unit> OpenCharacterScreen;

    public struct PlayManagerEvents
    {
        public static Action RefillSelectable;
    }
}
