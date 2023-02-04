using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Game.Scripts.Gameplay.Characters;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class StaticReferences : MonoBehaviour
{
    public static StaticReferences Instance;
    
    public Camera playerCamera;
    [SerializeField] private Transform player1spawnPosition;
    [SerializeField] private Transform player2spawnPosition;
    public Dictionary<CharacterTeam, Transform> playerSpawnPositions = new();

    private void Awake()
    {
        playerSpawnPositions.Add(CharacterTeam.team1, player1spawnPosition);
        playerSpawnPositions.Add(CharacterTeam.team2, player2spawnPosition);
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
