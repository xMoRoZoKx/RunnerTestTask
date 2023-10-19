using System.Collections;
using System.Collections.Generic;
using UniTools;
using UnityEngine;

/// The ability adds the specified speed in the speedPointsCount
[CreateAssetMenu(fileName = "ChangeSpeedAbility", menuName = "Abilities/Change Speed Ability")]
public class ChangeSpeedAbility : TemporaryAbility
{
    [SerializeField] private float speedPointsCount;
    public override void Apply(CharacterBaseView character)
    {
        var oldSpeed = character.CurrentSpeed.Value;

        character.SetSpeed(speedPointsCount);

        var factualPoints = character.CurrentSpeed.Value - oldSpeed;

        CreateTemporaryAbilityInstance(character, instance =>
        {
            character.SetSpeed(factualPoints);
        });
    }
}
