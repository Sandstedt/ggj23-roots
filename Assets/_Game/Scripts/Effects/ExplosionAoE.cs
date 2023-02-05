using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class ExplosionAoE : MonoBehaviour
    {
        [SerializeField] float radius;

        [SerializeField] int damage;

        private void Start()
        {
            RaycastHit hit;

            // Cast a sphere wrapping character controller 10 meters forward
            // to see if it is about to hit anything.
            if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, radius))
            {
                Debug.Log("HIT TARGET: " + hit.collider.transform.name);

                DestroyableObject hitTarget = hit.collider.transform.GetComponent<DestroyableObject>();
                if (hitTarget != null)
                {
                    hitTarget.Damage(damage, transform.position);

                }

                PlayerHealth hitPlayer = hit.transform.GetComponent<PlayerHealth>();
                if (hitPlayer != null)
                {
                    hitPlayer.Damage(damage, transform.position);
                }
            }
        }
    }
}