using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Bullet bulletPrefab = default;
    [SerializeField] private float cooldownTime = 0.4f;
    [SerializeField] private float spamCooldownTime = 0.2f;

    [Header("Shooting controls")] // Used to prevent hardcoding of keys (Use ProjectSettings > Input to change/set more keys of type)
    [SerializeField, Tooltip("Axes for shooting")] private string shootButton = "Shoot";

    private SoundManager soundManager;

    private float currentHoldCooldown = 0f;
    private float currentSpamCooldown = 0f;

    // Bug fix (0001): Multiple calls of GetButtonDown when it's not suppose to
    #if UNITY_WEBGL
    private bool buttonPressedFailSafe;
    #endif

    private void Awake() {
        soundManager = SoundManager.Instance;
    }

    private void Update()
    {
        currentSpamCooldown -= Time.deltaTime;

        // Bug fix (0001)
        #if UNITY_WEBGL
        if(!Input.GetButton(shootButton)) {
            if(buttonPressedFailSafe) {
                buttonPressedFailSafe = false;
            }
        }
        #endif

        if(currentSpamCooldown <= 0f) {
            if(HandleSpamInput()) {
                return;
            }
        }

        if(currentHoldCooldown > 0f) {
            currentHoldCooldown -= Time.deltaTime;
        } else {
            HandleInput();
        }
    }

    private bool HandleSpamInput() {
        if(Input.GetButtonDown(shootButton)) {

            // Bug fix (0001)
            #if UNITY_WEBGL
            if(buttonPressedFailSafe) {
                return false;
            }

            buttonPressedFailSafe = true;
            #endif

            currentSpamCooldown = spamCooldownTime;
            currentHoldCooldown = cooldownTime;

            soundManager.PlaySound("Shoot");

            Instantiate(bulletPrefab, transform.position, transform.rotation);

            return true;
        }

        return false;
    }

    private void HandleInput() {
        if(Input.GetButton(shootButton)) {
            currentHoldCooldown = cooldownTime;

            soundManager.PlaySound("Shoot");

            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
