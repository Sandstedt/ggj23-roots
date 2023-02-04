using _Game.Scripts;
using Assets._Game.Scripts.Gameplay.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay
{
    public class CharacterRespawner : MonoBehaviour
    {
        public int nrSpawned = 0;

        [SerializeField] CharacterTeam team;

        [SerializeField] List<GameObject> listPlayerModels;

        [SerializeField] PlayerController plrController;

        [SerializeField] Transform respawnPos;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RespawnCharacter();
            }
        }

        public void RespawnCharacter()
        {
            Debug.Log(" listPlayerModels.Count: " + listPlayerModels.Count);
            if (listPlayerModels != null && nrSpawned < listPlayerModels.Count)
            {
                SpawnNewCharacter();
                nrSpawned++;
            }
        }

        private void SpawnNewCharacter()
        {
            var plrModel = listPlayerModels[nrSpawned];
            plrController.RespawnPlayer(plrModel, respawnPos.position);
        }
    }
}