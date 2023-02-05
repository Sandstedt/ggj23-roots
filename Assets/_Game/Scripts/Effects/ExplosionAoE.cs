using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class ExplosionAoE : MonoBehaviour
    {
        [SerializeField] int damage;
        [SerializeField] float radius;

        private void Start()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in hitColliders)
            {
                if (hit.transform.TryGetComponent<DestroyableObject>(out var hitTarget))
                {
                    Debug.Log("Explosion AOE damage target: " + damage);
                    hitTarget.Damage(damage, hit.transform.position);
                }

                if (hit.transform.TryGetComponent<PlayerHealth>(out var hitPlayer))
                {
                    hitPlayer.Damage(damage, hit.transform.position);
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}