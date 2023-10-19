using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyAbility", menuName = "Abilities/Fly Ability")]
public class FlyAbility : Ability
{
    [SerializeField] private float duration;
    public override void Applay(CharacterBaseView character)
    {
        character.Fly(duration);
    }
}
