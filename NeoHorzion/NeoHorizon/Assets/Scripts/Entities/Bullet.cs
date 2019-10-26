using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 35f;

    private void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = transform.up * moveSpeed;
    }
}
