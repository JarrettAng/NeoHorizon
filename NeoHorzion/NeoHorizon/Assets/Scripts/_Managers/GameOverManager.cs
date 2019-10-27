﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private void Awake() {
        EventManager.OnGameOver += ShowGameOver;
    }

    private void ShowGameOver() {
        SoundManager.Instance.PlaySound("GameOver");


        Debug.Log("GAME OVER!!!!!!!!!!!!!!!!!!!!");

        Time.timeScale = 0;
    }
}
