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
    public bool ShooterLine;

    public void ToggleShooterLine(bool shooter) {
        if(shooter) {
            spriteRenderer.color = shooterColor;
            ShooterLine = true;
        } else {
            spriteRenderer.color = defaultColor;
            ShooterLine = false;
        }
    }
}
