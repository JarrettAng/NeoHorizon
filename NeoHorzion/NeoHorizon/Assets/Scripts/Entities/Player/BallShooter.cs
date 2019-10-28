using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallShootLine[] twoShootLines = default;
    [SerializeField] private Ball ballPrefab = default;
    [SerializeField] private Transform paddleBallObject = default;

    [Header("Attributes")]
    [SerializeField] private float swapTime = 0.75f;
    [SerializeField, Tooltip("Axes for shooting")] private string shootButton = "Shoot";

    [Header("Read-Only")]
    [SerializeField] private bool hasBall;

    private BallShootLine currentLine;

    private WaitForSeconds waitTime;

    private void Awake() {
        waitTime = new WaitForSeconds(swapTime);
        EventManager.OnBallDestroyed += ReviveBall;
    }

    private void Start() {
        hasBall = true;
        StartCoroutine(ShootLineSwapping());
    }

    private void Update() {
        if(hasBall) {
            HandleInput();
        }

        void HandleInput() {
            if(Input.GetButtonDown(shootButton)) {
                ShootBall();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ball")) {
            Destroy(other.gameObject);
            TogglePaddleBall(true);
        }
    }

    private void ReviveBall() {
        TogglePaddleBall(true);
    }

    private void ShootBall() {
        Ball newBall = Instantiate(ballPrefab, paddleBallObject.position, Quaternion.identity);
        newBall.Launch(currentLine.transform.up);

        TogglePaddleBall(false);
    }

    private void TogglePaddleBall(bool state) {
        hasBall = state;
        paddleBallObject.gameObject.SetActive(state);
        ToggleLines(state);
    }

    private void ToggleLines(bool state) {
        foreach(BallShootLine line in twoShootLines) {
            line.gameObject.SetActive(state);
        }
    }

    private IEnumerator ShootLineSwapping() {
        while(true) {
            twoShootLines[0].ToggleShooterLine(false);
            twoShootLines[1].ToggleShooterLine(true);
            currentLine = twoShootLines[1];

            yield return waitTime;

            twoShootLines[0].ToggleShooterLine(true);
            twoShootLines[1].ToggleShooterLine(false);
            currentLine = twoShootLines[0];

            yield return waitTime;
        }
    }
}
