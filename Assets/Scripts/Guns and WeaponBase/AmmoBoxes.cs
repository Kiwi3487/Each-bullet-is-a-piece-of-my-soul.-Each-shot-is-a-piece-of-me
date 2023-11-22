using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoBox : MonoBehaviour
{
    [SerializeField] private AudioSource pickUpAmmo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddAmmo();
            }
            Destroy(gameObject);
            pickUpAmmo.Play();
        }
    }
}