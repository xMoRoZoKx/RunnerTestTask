using System;
using System.Collections;
using System.Collections.Generic;
using UniTools;
using UnityEngine;

public abstract class TemporaryAbility : Ability
{
    [SerializeField] protected float duration;
    protected virtual AbilityInstance CreateTemporaryAbilityInstance(CharacterBaseView character, Action<AbilityInstance> onComplete)
    {
        var instance = new AbilityInstance(this);

        character.currentAbilities.Add(instance);

        character.Wait(duration, () =>
        {
            onComplete?.Invoke(instance);
            character.currentAbilities.Remove(instance);
        });
        
        return instance;
    }
}
