using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement attributes")]
    [SerializeField, Tooltip("How fast can the player move?")] private float moveSpeed = 15f;

    [Header("Movement controls")] // Used to prevent hardcoding of keys (Use ProjectSettings > Input to change/set more keys of type)
    [SerializeField, Tooltip("Axes for left / right movement")] private string horizontalMovement = "Horizontal";

    private Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        float direction = Input.GetAxisRaw(horizontalMovement);
        rb2d.velocity = new Vector2(direction * moveSpeed, 0);
    }
}
