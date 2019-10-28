using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] shootLines;
    [SerializeField] private SpriteRenderer[] shootLinesImage;

    [Header("Attributes")]
    [SerializeField] private float swapTime = 0.75f;

    private WaitForSeconds waitTime;

    private void Awake() {
        waitTime = new WaitForSeconds(swapTime);
    }

    private void Start() {
        StartCoroutine(ShootLineSwapping());
    }

    private IEnumerator ShootLineSwapping() {
        //while(true) {
            
        //}
    }
}
