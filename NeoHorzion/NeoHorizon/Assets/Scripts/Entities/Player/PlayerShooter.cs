using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private Bullet bulletPrefab = default;
    [SerializeField] private float cooldownTime =0.25f;

    [Header("Shooting controls")] // Used to prevent hardcoding of keys (Use ProjectSettings > Input to change/set more keys of type)
    [SerializeField, Tooltip("Axes for shooting")] private string shootButton = "Shoot";

    private float currentCooldown = 0f;

    private void Update()
    {
        if(currentCooldown > 0f) {
            currentCooldown -= Time.deltaTime;
        } else {
            HandleInput();
        }
    }

    private void HandleInput() {
        if(Input.GetButton(shootButton)) {
            currentCooldown += cooldownTime;

            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
