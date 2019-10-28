using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSwapper : Singleton<StartScreenSwapper>
{
    [Header("References")]
    [SerializeField] private GameObject mainPanel = default;
    [SerializeField] private GameObject nameSwapperPanel = default;
    [SerializeField] private GameObject highscoresPanel = default;
    [SerializeField] private GameObject creditsPanel = default;

    [Header("Other References")]
    [SerializeField] private GameObject quitWebText = default;

    // PSA: This entire script is hardcoded! Soz

    public void OpenMainPanel() {
        mainPanel.SetActive(true);

        nameSwapperPanel.SetActive(false);
        highscoresPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenNameSwapperPanel() {
        nameSwapperPanel.SetActive(true);

        mainPanel.SetActive(false);
        highscoresPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenHighscoresPanel() {
        highscoresPanel.SetActive(true);

        mainPanel.SetActive(false);
        nameSwapperPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenCreditsPanel() {
        creditsPanel.SetActive(true);

        mainPanel.SetActive(false);
        nameSwapperPanel.SetActive(false);
        highscoresPanel.SetActive(false);
    }

    public void QuitGame() {
        if(Application.platform == RuntimePlatform.WebGLPlayer || Application.isEditor) {
            quitWebText.SetActive(true);
            return;
        }

        Application.Quit();
    }
}
