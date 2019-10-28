﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 50f;

    private Rigidbody2D rb2d;

    public void Launch(Vector3 moveDirection) {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = moveDirection * speed;
    }
}
