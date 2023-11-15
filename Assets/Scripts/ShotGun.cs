using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bulletPrefab1;
    [SerializeField] private Rigidbody bulletPrefab2;
    [SerializeField] private int numBullets = 5;
    [SerializeField] private float spreadAngle = 20.0f;
    [SerializeField] private float force = 50;
    [SerializeField] private AudioSource ShootSound;

    protected override void Attack(float percent)
    {
        print("Firing shotgun with percent: " + percent);
        Ray camRay = InputManager.GetCameraRay();
        
        Rigidbody selectedBulletPrefab = percent > 0.5f ? bulletPrefab2 : bulletPrefab1;
        ShootSound.Play();

        for (int i = 0; i < numBullets; i++)
        {
            // Calculate a random direction within the spread angle
            Quaternion randomRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);
            
            Vector3 randomDirection = randomRotation * camRay.direction;
            Rigidbody rb = Instantiate(selectedBulletPrefab, camRay.origin, Quaternion.LookRotation(randomDirection));
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * randomDirection, ForceMode.Impulse);
            Destroy(rb.gameObject, 2);
        }
    }
}