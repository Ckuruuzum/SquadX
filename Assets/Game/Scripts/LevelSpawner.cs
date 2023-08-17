using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private StaminaHandler staminaHandler;

    [SerializeField] private float _spamTimer;
    private void Start()
    {
        staminaHandler = GetComponent<StaminaHandler>();
        _spamTimer = Random.Range(2, 10);
    }

    private void Update()
    {
        _spamTimer -= Time.deltaTime;
        if (_spamTimer < 0)
        {
            SpawnLevelUnit(LevelManager.Instance.stages[LevelManager.Instance.GetFaction()].level[1]);
        }
    }

    public void SpawnLevelUnit(Level level)
    {
        Unit tmpUnit = level.units[Random.Range(0, level.units.Length)];
        if (staminaHandler.GetEnemyStaminaValue() >= tmpUnit.unitStaminaCost && _spamTimer <= 0)
        {
            _spamTimer = Random.Range(3, 10);
            tmpUnit = level.units[Random.Range(0, level.units.Length)];
            UnitManager.instance.SpawnUnit(tmpUnit, UnitManager.TEAM.Enemy, tmpUnit.unitPrefab, Random.Range(0, 3));
            staminaHandler.DecreaseStaminaValue(staminaHandler.GetEnemyStaminaValue(), tmpUnit.unitStaminaCost, UnitManager.TEAM.Enemy);
        }
    }
}
