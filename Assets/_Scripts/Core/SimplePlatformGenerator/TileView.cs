using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    //Lenght in unity units
    [field: SerializeField] public float tileLenght { get; private set; }

    [field: SerializeField] public List<Transform> spawnPoints { get; private set; }
    
    //Interactables on tile instance
    public List<InteractableView> interactableViews { get; private set; } = new();
}
