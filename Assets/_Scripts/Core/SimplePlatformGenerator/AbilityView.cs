using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityView : InteractableView
{
    [SerializeField] protected Ability ability;
    protected override void OnInteract(CharacterBaseView character)
    {
        ability.Apply(character);
    }
}
