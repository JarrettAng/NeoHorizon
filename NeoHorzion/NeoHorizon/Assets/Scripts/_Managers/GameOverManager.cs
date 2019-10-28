using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gameOverPanel = default;

    [Header("Attributes")]
    [SerializeField, Tooltip("Axes for selecting")] private string quitButton = "Cancel";
    [SerializeField, Tooltip("Axes for selecting")] private string retryButton = "Shoot";
    [SerializeField] private float gameOverWaitSeconds = 1f;

    private bool gameOver = false;

    private bool gameOverPressedOnce = false;

    private void Awake() {
        EventManager.OnGameOver += ShowGameOver;
    }

    private void Update() {
        if(!gameOver) {
            if(Input.GetButtonDown(quitButton)) {
                if(gameOverPressedOnce) {
                    SceneSwapper.Instance.LoadStartScene();
                } else {
                    gameOverPressedOnce = true;
                    Invoke("ResetPressedOnce", gameOverWaitSeconds);
                }
            }
        } else {
            if(Input.GetButtonDown(retryButton)) {
                SceneSwapper.Instance.ReloadCurrentScene();
                Time.timeScale = 1;
            } else if(Input.GetButtonDown(quitButton)) {
                SceneSwapper.Instance.LoadStartScene();
                Time.timeScale = 1;
            }
        }
    }

    private void ShowGameOver() {
        SoundManager.Instance.PlaySound("GameOver");

        Time.timeScale = 0;

        gameOverPanel.SetActive(true);
        gameOver = true;
    }

    private void ResetPressedOnce() {
        gameOverPressedOnce = false;
    }
}
