using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPiece : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake() {
        soundManager = SoundManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        soundManager.PlaySound("Hit");

        Destroy(other.gameObject);
    }
}
