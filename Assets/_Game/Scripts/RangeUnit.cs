using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pathfinding;
using UnityEngine.UIElements;

public class RangeUnit : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ParticleSystem _muzzleFlash;

    private void Update()
    {
        /* if (GetComponent<AIDestinationSetter>().target != null)
         {
             _targetTransform.position = Vector3.Lerp(_targetTransform.position, new Vector3(GetComponent<AIDestinationSetter>().target.position.x, GetComponent<AIDestinationSetter>().target.position.y + 1f, GetComponent<AIDestinationSetter>().target.position.z), 0.1f);
             //_targetTransform.position = new Vector3(GetComponent<AIDestinationSetter>().target.position.x, 1, GetComponent<AIDestinationSetter>().target.position.z);
         }*/
    }

    public void AE_Shoot()
    {
        if (_muzzleFlash != null) _muzzleFlash.Play();

        Transform tmpTargetPos = GetComponent<AIDestinationSetter>().target;
        GameObject tmpProjectile = Instantiate(_projectile, _spawnPoint.position, Quaternion.identity);
        Vector3 shootDir = (tmpTargetPos.position - _spawnPoint.position).normalized;
        tmpProjectile.GetComponent<Projectile>().Setup(new Vector3(shootDir.x, shootDir.y + 0.2f, shootDir.z), tmpTargetPos, GetComponent<Health>());
    }
}
