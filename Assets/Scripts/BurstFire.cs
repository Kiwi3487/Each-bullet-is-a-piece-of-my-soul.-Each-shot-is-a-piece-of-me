using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFire : WeaponBase
{
    [SerializeField] private Rigidbody bulletPrefab3;
    [SerializeField] private float force = 50;
    [SerializeField] private float shotsPerBurst = 3;
    [SerializeField] private float burstDelay = 0.1f;
    private WaitForSeconds delay;
    [SerializeField] private AudioSource ShootSound;

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
            Rigidbody rb = Instantiate(bulletPrefab3, camRay.origin, Quaternion.LookRotation(shootDirection));
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * shootDirection, ForceMode.Impulse);
            ShootSound.Play();
            Destroy(rb.gameObject, 2);

            yield return delay;
        }
    }
}
