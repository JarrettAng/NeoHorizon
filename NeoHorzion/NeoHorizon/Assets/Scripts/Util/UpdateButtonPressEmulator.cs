using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateButtonPressEmulator : ButtonPressEmulator
{
    [Header("Update Attributes")]
    [SerializeField, Tooltip("Axes for selecting")] private string selectButton = "Shoot";
    [SerializeField] private float preventationWait = 0.1f;

    private bool pressPrevention = false;

    private void OnEnable() {
        pressPrevention = true;
        Invoke("TurnOffPrevention", preventationWait);
    }

    private void Update()
    {
        if(pressPrevention) return;

        if(Input.GetButtonDown(selectButton)) {
            Click();
        }
    }

    private void TurnOffPrevention() {
        pressPrevention = false;
    }
}
