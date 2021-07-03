using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    protected Collider2D col2d;
    [HideInInspector] public Rigidbody2D rb2d;

    private void Awake()
    {
        Initialization();
    }

    public virtual void Initialization()
    {
        col2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }
}
