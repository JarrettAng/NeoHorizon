using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float speed = 50f;
    [SerializeField] private int bouncesBeforeDestroy = 10;

    [Header("Read-Only")]
    [SerializeField] private int bounces = 0;

    private Rigidbody2D rb2d;

    public void Launch(Vector3 moveDirection) {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        bounces++;

        if(bounces >= bouncesBeforeDestroy) {
            EventManager.OnBallDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
