using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : Character,IKnockbackable
{
    protected Character character;

    private AgentMovement agentMovement;
    public override void Initialization()
    {
        base.Initialization();
        character = GetComponent<Character>();
        agentMovement = GetComponent<AgentMovement>();
    }

    public void Knockback(Vector2 direction, float power, float duration)
    {
        agentMovement.StartKnockback(direction, power, duration);
    }
}
