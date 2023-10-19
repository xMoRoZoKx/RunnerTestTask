using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [field: SerializeField] public float tileLenght { get; private set; }
    [field: SerializeField] public List<Transform> spawnPoints { get; private set; }
    public List<InteractableView> interactableViews { get; private set; } = new();
}
