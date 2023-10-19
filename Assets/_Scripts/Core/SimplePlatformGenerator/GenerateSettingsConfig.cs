using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SimpleGenerateSettings", menuName = "Configs/Generate Settings/Simple Generate Settings")]
public class GenerateSettingsConfig : ScriptableObject
{
    public TileView tilePrefab;
    public List<InteractableView> interactableViewsPrefabs;
    [Range(0, 100)] public int chansForSpawnInteractable;
    public float distanceToSpawnTile;
    public float distanceToReleaseTile;
    public float distanceToCorrectCoordinates;
}