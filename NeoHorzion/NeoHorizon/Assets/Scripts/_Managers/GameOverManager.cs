using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private void Awake() {
        EventManager.OnGameOver += ShowGameOver;
    }

    private void ShowGameOver() {
        Time.timeScale = 0;
    }
}
