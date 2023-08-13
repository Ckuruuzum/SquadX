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
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Quaternion _quaternionOffset;
    [SerializeField] private float _bulletSpeed = 0.25f;
    private Unit _unit;

    private void Start()
    {
        _unit = GetComponent<AI>().unit;
    }

    private void Update()
    {
        if (GetComponent<AIDestinationSetter>().target != null)
        {
            _targetTransform.position = Vector3.Lerp(_targetTransform.position, new Vector3(GetComponent<AIDestinationSetter>().target.position.x, GetComponent<AIDestinationSetter>().target.position.y + 1f, GetComponent<AIDestinationSetter>().target.position.z), 0.1f);
            //_targetTransform.position = new Vector3(GetComponent<AIDestinationSetter>().target.position.x, 1, GetComponent<AIDestinationSetter>().target.position.z);
        }
    }

    public void AE_Shoot()
    {
        if (_muzzleFlash != null) _muzzleFlash.Play();

        GameObject tmpProjectile = Instantiate(_projectile, _spawnPoint.position, _quaternionOffset, _spawnPoint);
        tmpProjectile.transform.position = _spawnPoint.position;
        Transform tmpTargetPos = GetComponent<AIDestinationSetter>().target;
        tmpProjectile.transform.DOMove(new Vector3(tmpTargetPos.position.x, 1, tmpTargetPos.position.z), _bulletSpeed).OnComplete(() =>
        {
            if (_hitEffect != null)
            {
                ParticleSystem tmpHitEffect = Instantiate(_hitEffect, tmpProjectile.transform.position, Quaternion.identity);
                GetComponent<Health>().AE_Damage();
            }

            Destroy(tmpProjectile);
        });
    }
}
