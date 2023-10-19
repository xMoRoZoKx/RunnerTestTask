using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniTools;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyAbility", menuName = "Abilities/Fly Ability")]
public class FlyAbility : TemporaryAbility
{
    public override void Apply(CharacterBaseView character)
    {
        character.state.value = CharacterState.Fly;

        CreateTemporaryAbilityInstance(character,
            onComplete: instance =>
            {
                if (character.currentAbilities.Any(abilityInst => abilityInst.Ability is FlyAbility && abilityInst != instance)) return;
                character.state.value = CharacterState.Run;
            });
    }
}
