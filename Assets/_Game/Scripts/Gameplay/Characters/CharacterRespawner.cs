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


        // [SerializeField] Transform respawnPos;

        // public Transform playerTransform;


        private void InstantiatePlayer()
        {
            // playerTransform = transform.parent;
            
            var p = Instantiate(playerPrefab, StaticReferences.Instance.playerSpawnPositions[team].position, StaticReferences.Instance.playerSpawnPositions[team].rotation);
            plrController = p.GetComponent<PlayerController>();
            plrController.playerHealth.characterRespawner = this;
            plrController.playerGrip = StaticReferences.Instance.playerGrips[team];
            plrController.playerCamera = transform.parent;
            
            if (StaticReferences.Instance.isKeyboard)
            {
                plrController.playerCamera = Camera.main.transform;
            }
            else
            {
                plrController.playerCamera = this.gameObject.transform.parent.transform;
            }

            Debug.Log("Instatiate " + p.name);
        }

        private void Start()
        {
            // respawnPos = StaticReferences.Instance.playerSpawnPositions[team];

            InstantiatePlayer();
            // if (team == CharacterTeam.team1)
            // {
            // }
            // else
            // {
            //     StartCoroutine(DelayInstantiate());
            // }

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
            plrController.RespawnPlayer(StaticReferences.Instance.playerSpawnPositions[team].position, StaticReferences.Instance.playerSpawnPositions[team].rotation);
        }

        // IEnumerator DelayInstantiate()
        // {
        //     yield return new WaitForSeconds(0.2f);
        //     InstantiatePlayer();
        // }
    }
}