using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Powerups
{
    public class PowerupPickup : MonoBehaviour
    {
        [SerializeField] GameObject weaponToPickup;

        [SerializeField] RandomSoundPlay randomSoundPlay;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Something entered a powerup!");
            randomSoundPlay.PlayRandomSound();
        }
    }
}