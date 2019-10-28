using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPieceHealth : PieceHealth
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ball")) {
            soundManager.PlaySound("Hit");

            if(!destroyed) {
                currentHealth--;
                UpdateHealthBar();

                Destroy(other.gameObject);

                EventManager.OnBallDestroyed?.Invoke();

                CheckIfDestroyed();
            }
        }
    }
}
