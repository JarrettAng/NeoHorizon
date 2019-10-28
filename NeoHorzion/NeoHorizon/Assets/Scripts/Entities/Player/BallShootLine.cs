using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShootLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Attributes")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color shooterColor;

    [Header("Read-Only")]
    [SerializeField] private bool shooterLine;

    public void ToggleShooterLine(bool shooter) {
        if(shooter) {
            spriteRenderer.color = shooterColor;
            shooterLine = true;
        } else {
            spriteRenderer.color = defaultColor;
            shooterLine = false;
        }
    }
}
