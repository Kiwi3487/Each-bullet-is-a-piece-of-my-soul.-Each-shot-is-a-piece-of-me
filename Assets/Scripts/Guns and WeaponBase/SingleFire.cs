using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class SingleFire : WeaponBase
{
    [SerializeField] private Rigidbody bulletPrefab4;
    [SerializeField] private float force = 50;
    [SerializeField] private AudioSource ShootSound;
   
    [SerializeField] private BulletSO stats;
    [SerializeField] private Rigidbody waterBullet;
    [SerializeField] private Rigidbody earthBullet;
    [SerializeField] private Rigidbody fireBullet;
    
    protected override void Attack(float percent)
    {
        Ray camRay = InputManager.GetCameraRay();
        Vector3 shootDirection = camRay.direction;

        if (stats.BulletParticles == EProjectileType.Water)
        {
            Rigidbody rb = Instantiate(waterBullet, camRay.origin, Quaternion.LookRotation(shootDirection));
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
            ShootSound.Play();
            Destroy(rb.gameObject, 2);
        }
        else if (stats.BulletParticles == EProjectileType.Earth)
        {
            Rigidbody rb = Instantiate(earthBullet, camRay.origin, Quaternion.LookRotation(shootDirection));
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
            ShootSound.Play();
            Destroy(rb.gameObject, 2);
        }
        else if (stats.BulletParticles == EProjectileType.Fire)
        {
            Rigidbody rb = Instantiate(fireBullet, camRay.origin, Quaternion.LookRotation(shootDirection));
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
            ShootSound.Play();
            Destroy(rb.gameObject, 2);
        }
    }
}
