using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFire : WeaponBase
{
    [SerializeField] private Rigidbody bulletPrefab4;
    [SerializeField] private float force = 50;
    [SerializeField] private AudioSource ShootSound;
    
    protected override void Attack(float percent)
    {
        Ray camRay = InputManager.GetCameraRay();
        Vector3 shootDirection = camRay.direction;
        
        Rigidbody rb = Instantiate(bulletPrefab4, camRay.origin, Quaternion.LookRotation(shootDirection));
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
        ShootSound.Play();
        Destroy(rb.gameObject, 2);
    }
}
