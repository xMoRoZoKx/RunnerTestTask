using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableView : MonoBehaviour
{
    public Action<CharacterBaseView> onInteract;
    protected virtual void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<CharacterBaseView>();
        if (character != null)
        {
            OnInteract(character);
            onInteract?.Invoke(character);
        }
    }
    protected virtual void OnInteract(CharacterBaseView character)
    {

    }
}
