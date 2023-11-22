using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    namespace ScriptableObjects
    {
        [Flags]
    public enum EProjectileType
        {
            Earth = 1,
            Water = 2,
            Fire = 4,
        }

        [CreateAssetMenu(menuName = "ShootDemo/BulletSO", fileName = "BulletSO", order = 1)]

        public class BulletSO : ScriptableObject
        {
            [field: SerializeField] public EProjectileType BulletParticles { get; private set; }

            public void SetBulletParticles(EProjectileType newBulletParticles)
            {
                BulletParticles = newBulletParticles;
            }
        }
    }