﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovingPiece pieceComponent = default;
    [SerializeField] private Transform healthBar = default;

    [Header("Attributes")]
    [SerializeField] private int maxHealth = 5;

    [Header("Read-Only")]
    [SerializeField] private int currentHealth;
    [SerializeField] private bool destroyed = false;

    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);

        if(!destroyed) {
            currentHealth--;
            UpdateHealthBar();

            CheckIfDestroyed();
        }
    }

    private void CheckIfDestroyed() {
        if(currentHealth <= 0) {
            pieceComponent.HandlePieceDestroyed();
            destroyed = true;
        }
    }

    private void UpdateHealthBar() {
        Vector2 newScale = healthBar.localScale;

        newScale.x = (float)currentHealth / (float)maxHealth;

        healthBar.transform.localScale = newScale;
    }
}
