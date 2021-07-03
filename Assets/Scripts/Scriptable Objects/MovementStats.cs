using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agents/Movement Data")]
public class MovementStats : ScriptableObject
{
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
}
