using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallShootLine[] threeShootLines;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform paddleBallObject;

    [Header("Attributes")]
    [SerializeField] private float swapTime = 0.75f;
    [SerializeField, Tooltip("Axes for shooting")] private string shootButton = "Shoot";

    [Header("Read-Only")]
    [SerializeField] private bool hasBall;

    private BallShootLine currentShootLine;

    private WaitForSeconds waitTime;

    private void Awake() {
        waitTime = new WaitForSeconds(swapTime);
    }

    private void Start() {
        StartCoroutine(ShootLineSwapping());
    }

    private void Update() {
        if(hasBall) {
            HandleInput();
        }

        void HandleInput() {
            if(Input.GetButton(shootButton)) {
                ShootBall();
            }
        }
    }

    private void ShootBall() {
        Ball newBall = Instantiate(ballPrefab, paddleBallObject.position, Quaternion.identity);
        newBall.Launch();

        TogglePaddleBall(false);
    }

    private void TogglePaddleBall(bool state) {
        hasBall = state;
        paddleBallObject.gameObject.SetActive(state);
    }

    private IEnumerator ShootLineSwapping() {
        while(true) {
            threeShootLines[1].ToggleShooterLine(false);
            threeShootLines[0].ToggleShooterLine(true);
            currentShootLine = threeShootLines[0];

            yield return waitTime;

            threeShootLines[0].ToggleShooterLine(false);
            threeShootLines[1].ToggleShooterLine(true);
            currentShootLine = threeShootLines[1];

            yield return waitTime;

            threeShootLines[1].ToggleShooterLine(false);
            threeShootLines[2].ToggleShooterLine(true);
            currentShootLine = threeShootLines[2];

            yield return waitTime;

            threeShootLines[2].ToggleShooterLine(false);
            threeShootLines[1].ToggleShooterLine(true);
            currentShootLine = threeShootLines[1];

            yield return waitTime;
        }
    }
}
