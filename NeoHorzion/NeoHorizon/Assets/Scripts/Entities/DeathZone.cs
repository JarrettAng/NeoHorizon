﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(other.gameObject);

        EventManager.OnBallDestroyed?.Invoke();
        SoundManager.Instance.PlaySound("Hit");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }
}
