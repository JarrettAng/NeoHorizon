using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSwapper : Singleton<StartScreenSwapper>
{
    [Header("References")]
    [SerializeField] private GameObject mainPanel = default;
    [SerializeField] private GameObject nameSwapperPanel = default;

    // PSA: This entire script is hardcoded! Soz

    public void OpenMainPanel() {
        mainPanel.SetActive(true);

        nameSwapperPanel.SetActive(false);
    }

    public void OpenNameSwapperPanel() {
        nameSwapperPanel.SetActive(true);

        mainPanel.SetActive(false);
    }
}
