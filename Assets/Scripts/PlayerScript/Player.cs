using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public ScriptableObjects.BulletSO bulletSO;
    [SerializeField] private WeaponBase shotGunWeapon;
    [SerializeField] private WeaponBase burstFireWeapon;
    [SerializeField] private WeaponBase singleFireWeapon;
    private bool weaponShootToggle;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayers;
    
    private Vector2 currentRotation;
    //Camera movement
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;
    
    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;
    [SerializeField] private Transform followTarget;

    [SerializeField] private AudioSource SwitchWeaponSound;

    private Vector2 currentAngle;

    private bool isGrounded;
    private Vector3 _moveDirection;
    private Rigidbody rb;
    private float depth;
    
    private int maxBullets = 15;
    private int minBullets = 0;
    private int currentBullets = 15;

    private int selectedGun = 1;
    private string currentWeapon;
    private string currentElement;

    
    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();
        rb = GetComponent<Rigidbody>();
        depth = GetComponent<Collider>().bounds.size.y;
    }
    
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        CheckGround();
        SwitchBullets();
    }
    
    public void SetLookRotation(Vector2 readValue)
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensY;
        currentAngle.y = Mathf.Clamp(currentAngle.y, minViewAngle, maxViewAngle);
        
        
        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    }

    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, depth,groundLayers);
        Debug.DrawRay(transform.position, Vector3.down * depth,
            Color.green, 0, false);
    }
    
    public void Shoot()
    {
        if (currentBullets > 0)
        {
            if (selectedGun == 1)
            {
                weaponShootToggle = !weaponShootToggle;
                if(weaponShootToggle) shotGunWeapon.StartShooting();
                else shotGunWeapon.StopShooting();
                --currentBullets;
            }
            else if (selectedGun == 2)
            {
                weaponShootToggle = !weaponShootToggle;
                if(weaponShootToggle) burstFireWeapon.StartShooting();
                else burstFireWeapon.StopShooting();
                --currentBullets;
            }
            else if(selectedGun == 3)
            {
                weaponShootToggle = !weaponShootToggle;
                if(weaponShootToggle) singleFireWeapon.StartShooting();
                else singleFireWeapon.StopShooting();
                --currentBullets;
            }
        }
    }
    
    public void Reload()
    {
        int bulletAmount = maxBullets - currentBullets;
        currentBullets += bulletAmount;
        Debug.Log("Shoot MOAR");
    }
    
    public void AddAmmo()
    {
        maxBullets += 5;
    }
    
    public int GetCurrentBullets()
    {
        return currentBullets;
    }
    public int GetMaxBullets()
    {
        return maxBullets;
    }

    public string GetCurrentGun()
    {
        return currentWeapon;
    }


    public void ShotGunWeapon()
    {
        selectedGun = 1;
        currentWeapon = "Charging ShotGun";
        SwitchWeaponSound.Play();
    }

    public void BurstFireWeapon()
    {
        selectedGun = 2;
        currentWeapon = "Burst Fire Gun";
        SwitchWeaponSound.Play();
    }

    public void SingleFireWeapon()
    {
        selectedGun = 3;
        currentWeapon = "Single Shot Gun";
        SwitchWeaponSound.Play();
    }
    
    
    public void SwitchBullets()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bulletSO.SetBulletParticles(ScriptableObjects.EProjectileType.Earth);
            currentElement = "Earth";
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            bulletSO.SetBulletParticles(ScriptableObjects.EProjectileType.Water);
            currentElement = "Water";
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            bulletSO.SetBulletParticles(ScriptableObjects.EProjectileType.Fire);
            currentElement = "Fire";
        }
    }
    public string GetCurrentElement()
    {
        return currentElement;
    }
}
