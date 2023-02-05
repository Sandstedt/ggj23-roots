using System;
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
        [SerializeField] private GameObject playerPrefab;


        [SerializeField] Transform respawnPos;


        private void Start()
        {
            respawnPos = StaticReferences.Instance.playerSpawnPositions[team];
            var p = Instantiate(playerPrefab, respawnPos.position, respawnPos.rotation);
            plrController = p.GetComponent<PlayerController>();
            plrController.transform.position = respawnPos.position;
            plrController.transform.rotation = respawnPos.rotation;
            plrController.playerHealth.characterRespawner = this;
            plrController.playerGrip = StaticReferences.Instance.playerGrips[team];
            if (StaticReferences.Instance.isKeyboard)
            {
                plrController.playerCamera = Camera.main;
            }
            else
            {
                plrController.playerCamera = this.gameObject.transform.parent.transform.Find("Left Eye Camera")
                .GetComponent<Camera>();
            }

            Debug.Log("Instatiate " + p.name);
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