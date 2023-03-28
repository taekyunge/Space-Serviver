﻿using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        if (rigidbody == null)
            return;

        rigidbody.MovePosition(rigidbody.position + velocity);
    }

    public void AddForce(Vector2 force)
    {
        if (rigidbody == null)
            return;

        rigidbody.AddForce(force);
    }
}
