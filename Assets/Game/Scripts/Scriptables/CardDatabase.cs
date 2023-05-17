using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Unit", menuName = "SquadX/Database")]
public class CardDatabase : ScriptableObject
{
    [Header("Human")]
    public Sprite cardBackHuman;
    public Color32 bottomGlowHuman;
    public Color32 tierTextColorHuman;
    public Color32 tierTextGlowHuman;
    public Color32 bottomBarColorHuman;

    [Header("Demon")]
    public Sprite cardBackDemon;
    public Color32 bottomGlowDemon;
    public Color32 tierTextColorDemon;
    public Color32 tierTextGlowDemon;
    public Color32 bottomBarColorDemon;

    [Header("Zombie")]
    public Sprite cardBackZombie;
    public Color32 bottomGlowZombie;
    public Color32 tierTextColorZombie;
    public Color32 tierTextGlowZombie;
    public Color32 bottomBarColorZombie;




}
