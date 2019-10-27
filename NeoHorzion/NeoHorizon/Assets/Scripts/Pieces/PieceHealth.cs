using System.Collections;
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

    private SoundManager soundManager;

    private void Awake() {
        soundManager = SoundManager.Instance;
    }

    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        soundManager.PlaySound("Hit");

        Destroy(other.gameObject);

        if(!destroyed) {
            currentHealth--;
            UpdateHealthBar();

            CheckIfDestroyed();
        }
    }

    /// <summary>
    /// Runs when a piece gets added to the static blob
    /// </summary>
    public void HandlePieceAttached() {
        healthBar.gameObject.SetActive(false);
        destroyed = true;
    }

    private void CheckIfDestroyed() {
        if(currentHealth <= 0) {
            soundManager.PlaySound("Zoom");
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
