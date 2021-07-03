using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : Character
{
    protected Character character;

    protected override void Initialization()
    {
        base.Initialization();
        character = GetComponent<Character>();
    }
}
