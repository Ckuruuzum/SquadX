using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaHandler : MonoBehaviour
{
    [Header("Player")]
    public Slider playerStamina;
    [SerializeField] private float _playerStaminaValue;
    [SerializeField] private TextMeshProUGUI playerStaminaText;
    [Space]
    [Header("Enemy")]
    [SerializeField] private float enemyStaminaValue;
    private int maxStaminaValue = 15;

    private float playerStaminaRounded;
    private void Update()
    {
        if (_playerStaminaValue < maxStaminaValue)
        {
            _playerStaminaValue = _playerStaminaValue + Time.deltaTime / 2;
            playerStamina.value = _playerStaminaValue;
            playerStaminaRounded = _playerStaminaValue;
            playerStaminaText.text = Mathf.Floor(playerStaminaRounded).ToString();
        }

        if (enemyStaminaValue < maxStaminaValue)
        {
            enemyStaminaValue = enemyStaminaValue + Time.deltaTime / 2;
        }


    }

    public bool CheckStamina(float staminaValue, int requiredStamina)
    {
        if (requiredStamina > staminaValue) return false;
        else return true;
    }

    public float GetPlayerStaminaValue()
    {
        return _playerStaminaValue;
    }

    public float GetEnemyStaminaValue()
    {
        return enemyStaminaValue;
    }

    public void DecreaseStaminaValue(float staminaValue, int requiredStamina, UnitManager.TEAM team)
    {
        staminaValue = staminaValue - requiredStamina;
        if (team == UnitManager.TEAM.Ally) _playerStaminaValue = staminaValue;
        else enemyStaminaValue = staminaValue;
    }

    public void IncreaseStaminaValue(float staminaValue, int increaseValue)
    {
        staminaValue = staminaValue + increaseValue;
    }
}
