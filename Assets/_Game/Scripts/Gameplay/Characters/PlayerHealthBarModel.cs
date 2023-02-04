using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Characters
{
    public class PlayerHealthBarModel : MonoBehaviour
    {
        [SerializeField] List<GameObject> listHealthModels;
        [SerializeField] List<ParticleSystem> listParticles;

        public void Restore()
        {
            for (int i = 0; i < 4; i++)
            {
                listHealthModels[i].gameObject.SetActive(true);
            }
        }

        public void PlayerDamaged(int healthAfterDamage)
        {
            //This is a jam and this is a shitty code, but here goes
            int scale = healthAfterDamage % 4;
            Debug.Log("scale: " + scale);

            if (scale == 3)
            {
                listHealthModels[3].SetActive(false);
                listParticles[3].Play();
            }

            if (scale == 2)
            {
                listHealthModels[2].SetActive(false);
                listParticles[2].Play();
            }

            if (scale == 1)
            {
                listHealthModels[1].SetActive(false);
                listParticles[1].Play();
            }

            if (scale == 0)
            {
                listHealthModels[0].SetActive(false);
                listParticles[0].Play();
            }
        }
    }
}