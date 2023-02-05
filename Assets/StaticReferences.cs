using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Gameplay.Characters;
using Assets._Game.Scripts.Gameplay.Characters;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class StaticReferences : MonoBehaviour
{
    public static StaticReferences Instance;
    public bool isKeyboard = false;

    public Camera playerCamera;
    [SerializeField] private Transform player1spawnPosition;
    [SerializeField] private Transform player2spawnPosition;
    public Dictionary<CharacterTeam, Transform> playerSpawnPositions = new();
    [SerializeField] private PlayerGrip playerGripP1;
    [SerializeField] private PlayerGrip playerGripP2;
    public Dictionary<CharacterTeam, PlayerGrip> playerGrips = new();
    

    private void Awake()
    {
        playerSpawnPositions.Add(CharacterTeam.team1, player1spawnPosition);
        playerSpawnPositions.Add(CharacterTeam.team2, player2spawnPosition);
        playerGrips.Add(CharacterTeam.team1, playerGripP1);
        playerGrips.Add(CharacterTeam.team2, playerGripP2);

        if (isKeyboard)
        {
            playerCamera.transform.position += playerCamera.transform.forward * -40;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
