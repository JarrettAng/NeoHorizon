using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSwapper : Singleton<StartScreenSwapper>
{
    [Header("References")]
    [SerializeField] private GameObject mainPanel = default;
    [SerializeField] private GameObject nameSwapperPanel = default;
    [SerializeField] private GameObject highscoresPanel = default;

    // PSA: This entire script is hardcoded! Soz

    public void OpenMainPanel() {
        mainPanel.SetActive(true);

        nameSwapperPanel.SetActive(false);
        highscoresPanel.SetActive(false);
    }

    public void OpenNameSwapperPanel() {
        nameSwapperPanel.SetActive(true);

        mainPanel.SetActive(false);
        highscoresPanel.SetActive(false);
    }

    public void OpenHighscoresPanel() {
        highscoresPanel.SetActive(true);

        mainPanel.SetActive(false);
        nameSwapperPanel.SetActive(false);
    }
}
