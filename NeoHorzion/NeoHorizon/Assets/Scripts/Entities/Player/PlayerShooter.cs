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

    private void Awake() {
        soundManager = SoundManager.Instance;
    }

    private void Update()
    {
        currentSpamCooldown -= Time.deltaTime;

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
