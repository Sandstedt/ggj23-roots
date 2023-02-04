using Assets._Game.Scripts.Gameplay.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay
{
    public class CharacterRespawner : MonoBehaviour
    {
        private int nrSpawned = 0;

        [SerializeField] CharacterTeam team;

        [SerializeField] List<GameObject> listCharacters;

        public void RespawnCharacter()
        {
            nrSpawned++;
            if (listCharacters != null && listCharacters.Count < nrSpawned)
            {
                SpawnNewCharacter();
            }
        }

        private void SpawnNewCharacter()
        {
            var character = listCharacters[nrSpawned];
        }
    }
}