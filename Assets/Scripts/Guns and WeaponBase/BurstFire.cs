using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class BurstFire : WeaponBase
{
    [SerializeField] private Rigidbody bulletPrefab3;
    [SerializeField] private float force = 50;
    [SerializeField] private float shotsPerBurst = 3;
    [SerializeField] private float burstDelay = 0.1f;
    private WaitForSeconds delay;
    [SerializeField] private AudioSource ShootSound;
    
    [SerializeField] private BulletSO stats;
    [SerializeField] private Rigidbody waterBullet;
    [SerializeField] private Rigidbody earthBullet;
    [SerializeField] private Rigidbody fireBullet;

    protected override void Attack(float percent)
    {
        delay = new WaitForSeconds(burstDelay);
        StartCoroutine(Burst(percent));
    }

    private IEnumerator Burst(float percent)
    {
        Ray camRay = InputManager.GetCameraRay();
        Vector3 shootDirection = camRay.direction;
        for (int i = 0; i < shotsPerBurst; i++)
        {
            if (stats.BulletParticles == EProjectileType.Water)
            {
                Rigidbody rb = Instantiate(waterBullet, camRay.origin, Quaternion.LookRotation(shootDirection));
                rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
                ShootSound.Play();
                Destroy(rb.gameObject, 2);

                yield return delay;
            }
            else if (stats.BulletParticles == EProjectileType.Earth)
            {
                Rigidbody rb = Instantiate(earthBullet, camRay.origin, Quaternion.LookRotation(shootDirection));
                rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
                ShootSound.Play();
                Destroy(rb.gameObject, 2);

                yield return delay;
            }
            else if (stats.BulletParticles == EProjectileType.Fire)
            {
                Rigidbody rb = Instantiate(fireBullet, camRay.origin, Quaternion.LookRotation(shootDirection));
                rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
                ShootSound.Play();
                Destroy(rb.gameObject, 2);

                yield return delay;
            }
        }
    }
}
