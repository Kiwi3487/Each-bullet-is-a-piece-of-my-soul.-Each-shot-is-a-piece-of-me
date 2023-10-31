using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bullet1;
    [SerializeField] private Rigidbody bullet2;
    [SerializeField] private float force = 50;
    protected override void Attack(float percent)
    {
        print("Attacking with percent: " + percent);
        Ray camRay = InputManager.GetCameraRay();
        Rigidbody rb = Instantiate(percent > 0.5f ? bullet2 : bullet1, camRay.origin, transform.rotation);
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * camRay.direction, ForceMode.Impulse);
        Destroy(rb.gameObject, 2);
    }
}