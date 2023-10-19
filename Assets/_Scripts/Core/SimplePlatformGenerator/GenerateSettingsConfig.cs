using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Settings config for generate route
[CreateAssetMenu(fileName = "SimpleGenerateSettings", menuName = "Configs/Generate Settings/Simple Generate Settings")]
public class GenerateSettingsConfig : ScriptableObject
{
    public TileView tilePrefab;
    // Possible for generation
    public List<InteractableView> interactableViewsPrefabs;
    // spawn chance per 1 spawnPoint from tile view
    [Range(0, 100)] public int chansForSpawnInteractable;
    public float distanceToSpawnTile;
    public float distanceToReleaseTile;
    // to avoid coordinate errors during a long game
    public float distanceToCorrectCoordinates;
}