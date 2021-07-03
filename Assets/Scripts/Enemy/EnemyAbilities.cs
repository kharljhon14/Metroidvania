using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities : EnemyCharacter
{
    protected EnemyCharacter enemyCharacter;

    public override void Initialization()
    {
        base.Initialization();
        enemyCharacter = GetComponent<EnemyCharacter>();
    }
}
