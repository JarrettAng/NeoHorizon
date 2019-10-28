using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateButtonPressEmulator : ButtonPressEmulator
{
    [Header("Update Attributes")]
    [SerializeField, Tooltip("Axes for selecting")] private string selectButton = "Shoot";

    private void Update()
    {
        if(Input.GetButtonDown(selectButton)) {
            Click();
        }
    }
}
