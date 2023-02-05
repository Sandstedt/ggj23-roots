using System;
using System.Collections;
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


        private void InstantiatePlayer()
        {
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

        private void Start()
        {
            respawnPos = StaticReferences.Instance.playerSpawnPositions[team];

            if (team == CharacterTeam.team1)
            {
                InstantiatePlayer();
            }
            else
            {
                StartCoroutine(DelayInstantiate());
            }

        }

        public void RespawnCharacter()
        {
            Debug.Log("Player is respawning");
            if (listPlayerModels != null && nrSpawned < listPlayerModels.Count)
            {
                SpawnNewCharacter();
                nrSpawned++;
            }
            else
            {
                nrSpawned = 0;
                SpawnNewCharacter();

            }
        }

        private void SpawnNewCharacter()
        {
            plrController.RespawnPlayer(respawnPos.position);
        }

        IEnumerator DelayInstantiate()
        {
            yield return new WaitForSeconds(0.2f);
            InstantiatePlayer();
        }
    }
}