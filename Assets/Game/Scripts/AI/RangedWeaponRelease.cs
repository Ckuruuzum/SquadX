using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponRelease : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    private Rigidbody _weaponRigidbody;
    private Collider _weaponCollider;

    private void Start()
    {
        _weaponRigidbody = _weapon.GetComponent<Rigidbody>();
        _weaponCollider = _weapon.GetComponent<Collider>();
    }

    public void AE_ReleaseWeapon()
    {
        _weapon.transform.parent = null;
        _weaponRigidbody.isKinematic = false;
        _weaponCollider.enabled = true;
        Destroy(_weapon, 2);
    }
}
