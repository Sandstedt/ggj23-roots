using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Characters
{
    public class PlayerHealthBarModel : MonoBehaviour
    {
        public CharacterTeam team;
        [SerializeField] List<GameObject> listHealthModels;
        [SerializeField] List<ParticleSystem> listParticles;
        [SerializeField] private int lives;
        private int totalLives = 3;

        private void Start()
        {
            lives = totalLives;
            for (int i = 0; i < 4; i++)
            {
                listHealthModels[i].gameObject.SetActive(true);
            }
        }

        public void Restore()
        {
            return;
            
            Debug.Log("Restore");
            lives = totalLives;
            for (int i = 0; i < 4; i++)
            {
                listHealthModels[i].gameObject.SetActive(true);
            }
        }

        public void Die()
        {
            if (lives < 0)
            {
                return;
            }
            listHealthModels[lives].SetActive(false);
            listParticles[lives].Play();
            lives--;
            if (lives == -1)
            {
                StaticReferences.Instance.winTxt.text = "Player " + (team == CharacterTeam.team1 ? "2" : "1") + " won!";
                StaticReferences.Instance.winTxt.gameObject.SetActive(true);
                StaticReferences.Instance.RestartSceneDelay();
            }
        }

        public void PlayerDamaged(int healthAfterDamage)
        {
            return;
            
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