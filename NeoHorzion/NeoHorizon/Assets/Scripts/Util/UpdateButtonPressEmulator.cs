using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateButtonPressEmulator : ButtonPressEmulator
{
    [Header("Update Attributes")]
    [SerializeField, Tooltip("Axes for selecting")] private string selectButton = "Shoot";

    #if UNITY_WEBGL
    [SerializeField] private float preventationWait = 0.1f;

    private bool pressPrevention = false;

    private void OnEnable() {
        pressPrevention = true;
        Invoke("TurnOffPrevention", preventationWait);
    }

    private void TurnOffPrevention() {
        pressPrevention = false;
    }
    #endif




    private void Update()
    {
        #if UNITY_WEBGL
        if(pressPrevention) return;
        #endif

        if(Input.GetButtonDown(selectButton)) {
            Click();
        }
    }


}
