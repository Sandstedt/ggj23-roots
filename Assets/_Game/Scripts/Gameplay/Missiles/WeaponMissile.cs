using Assets._Game.Scripts.Gameplay.Characters;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Missiles
{
    public class WeaponMissile : MonoBehaviour
    {
        public int damage = 5;

        [SerializeField] float timedLife = 10f;

        [SerializeField] Collider collisionCollider;
        [SerializeField] GameObject SpawnOnDeath;

        public void SetIgnoreObject(Collider plrCollider)
        {
            Physics.IgnoreCollision(plrCollider, collisionCollider);

        }

        void Awake()
        {
            //Start the coroutine we define below named ExampleCoroutine.
            StartCoroutine(DieAfterTime());
        }

        IEnumerator DieAfterTime()
        {
            yield return new WaitForSeconds(timedLife);
            DestroyMissile();
        }

        public void MissileHitObject()
        {
            collisionCollider.enabled = false;
            DestroyMissile();
        }

        private void DestroyMissile()
        {
            if (SpawnOnDeath != null)
            {
                Instantiate(SpawnOnDeath, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}