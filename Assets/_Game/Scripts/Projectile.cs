using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 100f;
    [SerializeField] private Vector3 _prefabOffset;
    [SerializeField] private ParticleSystem _hitEffect;
    private Vector3 _shootDir;
    private GameObject _target;
    private bool isHit;
    private Health _health;
    public void Setup(Vector3 shootDir, Transform target, Health health)
    {
        this._shootDir = shootDir;
        Vector3 targetDirection = target.position - transform.position;
        float singleStep = 1000 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.eulerAngles = _prefabOffset;
        _target = target.transform.gameObject;
        _health = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            transform.position += _shootDir * Time.deltaTime * projectileSpeed;
        }
        Destroy(gameObject, 3);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (_target.TryGetComponent(out Collider collider))
        {
            if (other == collider)
            {
                isHit = true;
                if (_hitEffect != null) Instantiate(_hitEffect, transform.position, Quaternion.identity);
                _health.AE_Damage();
                Destroy(gameObject);
            }
        }


    }
}
