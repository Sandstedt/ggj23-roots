﻿using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Missiles
{
    public class WeaponMissile : MonoBehaviour
    {
        public int damage = 5;

        [SerializeField] Collider collisionCollider;
        void Awake()
        {
            //Start the coroutine we define below named ExampleCoroutine.
            StartCoroutine(DieAfterTime());
        }

        IEnumerator DieAfterTime()
        {
            yield return new WaitForSeconds(10);
            DestroyMissile();
        }

        public void MissileHitObject()
        {
            collisionCollider.enabled = false;
            DestroyMissile();
        }

        private void DestroyMissile()
        {
            Destroy(gameObject);

        }
    }
}