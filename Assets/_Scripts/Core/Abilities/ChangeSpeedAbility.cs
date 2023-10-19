using System.Collections;
using System.Collections.Generic;
using UniTools;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeedAbility", menuName = "Abilities/Change Speed Ability")]
public class ChangeSpeedAbility : Ability
{
    [SerializeField] private float speedPointsCount;
    [SerializeField] private float duration;
    public override void Applay(CharacterBaseView character)
    {
        var factualPoints = character.AddSpeedAndGetFactualOffset(speedPointsCount);

        character.Wait(duration, () =>
        {
            character.AddSpeedAndGetFactualOffset(factualPoints);
        });
    }
}
