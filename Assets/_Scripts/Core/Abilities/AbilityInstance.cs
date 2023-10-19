using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInstance
{
    protected Ability _ability;
    public Ability Ability => _ability;
    public AbilityInstance(Ability ability)
    {
        _ability = ability;
    }
}
