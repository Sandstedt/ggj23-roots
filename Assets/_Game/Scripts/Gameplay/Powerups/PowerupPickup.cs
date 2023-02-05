using _Game.Scripts;
using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Powerups
{
    public class PowerupPickup : MonoBehaviour
    {
        [SerializeField] WeaponType weaponType;
        [SerializeField] RandomSoundPlay randomSoundPlay;

        [SerializeField] float respawnTime = 30f;

        [SerializeField] GameObject weaponModel;
        [SerializeField] Collider pickupCollider;

        [SerializeField] ParticleSystem particlePickup;

        private void OnTriggerEnter(Collider other)
        {
            randomSoundPlay.PlayRandomSound();

            var plr = other.GetComponent<PlayerController>();
            if (plr != null)
            {
                Debug.Log("plr: " + plr.name);
                plr.WeaponPickup(weaponType);
                weaponModel.SetActive(false);
                pickupCollider.enabled = false;
                particlePickup.Play();
                StartCoroutine(RestoreWeaponPickup());
            }
        }

        IEnumerator RestoreWeaponPickup()
        {
            yield return new WaitForSeconds(respawnTime);
            weaponModel.SetActive(true);
            particlePickup.Play();
            pickupCollider.enabled = true;
        }
    }
}