using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Collider2D collider2D;
    protected Rigidbody2D rigidbody2D;

    private void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
