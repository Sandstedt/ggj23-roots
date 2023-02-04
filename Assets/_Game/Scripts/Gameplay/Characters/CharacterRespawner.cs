using _Game.Scripts;
using Assets._Game.Scripts.Gameplay.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay
{
    public class CharacterRespawner : MonoBehaviour
    {
        private int nrSpawned = 0;

        [SerializeField] CharacterTeam team;

        [SerializeField] List<GameObject> listPlayerModels;

        [SerializeField] PlayerController plrController;

        public void RespawnCharacter()
        {
            nrSpawned++;
            if (listPlayerModels != null && listPlayerModels.Count < nrSpawned)
            {
                SpawnNewCharacter();
            }
        }

        private void SpawnNewCharacter()
        {
            var plrModel = listPlayerModels[nrSpawned];
            plrController.SetModel(plrModel);
        }
    }
}