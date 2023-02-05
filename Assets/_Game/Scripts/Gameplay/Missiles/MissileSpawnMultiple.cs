using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class MissileSpawnMultiple : MonoBehaviour
    {
        [SerializeField] Rigidbody[] listBodies;
        [SerializeField] float firingForceRight, firingForceUp;

        private void Start()
        {
            foreach (Rigidbody body in listBodies)
            {
                body.AddForce(body.transform.right * firingForceRight);
                body.AddForce(body.transform.up * firingForceUp);
            }
        }
    }
}