using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ButtonPressEmulator swapNameButton = default;
    [SerializeField] private ButtonPressEmulator highScoresButton = default;
    [SerializeField] private ButtonPressEmulator playShooterButton = default;
    [SerializeField] private ButtonPressEmulator playBallButton = default;
    [SerializeField] private ButtonPressEmulator creditsButton = default;
    [SerializeField] private ButtonPressEmulator quitButton = default;
    [SerializeField] private RectTransform selector = default;

    [Header("Attributes")]
    [SerializeField, Tooltip("Axes for left / right movement")] private string horizontalMovement = "Horizontal";
    [SerializeField, Tooltip("Axes for up / down movement")] private string verticalMovement = "Vertical";
    [SerializeField, Tooltip("Axes for selecting")] private string selectButton = "Shoot";

    [Header("Read-Only")]
    [SerializeField] private Vector2Int currentIndex;

    private ButtonPressEmulator[,] buttonsGrid;

    // PSA: This entire script is hardcoded

    private void Awake() {
        buttonsGrid = new ButtonPressEmulator[2, 3];

        buttonsGrid[0, 2] = swapNameButton;
        buttonsGrid[1, 2] = highScoresButton;
        buttonsGrid[0, 1] = playShooterButton;
        buttonsGrid[1, 1] = playBallButton;
        buttonsGrid[0, 0] = creditsButton;
        buttonsGrid[1, 0] = quitButton;

        currentIndex = new Vector2Int(0, 2);
    }

    private void Update() {
        if(Input.GetButtonDown(horizontalMovement)) {
            if(Input.GetAxisRaw(horizontalMovement) > 0) {
                MoveRight();
            } else {
                MoveLeft();
            }

            return;
        }

        if(Input.GetButtonDown(verticalMovement)) {
            if(Input.GetAxisRaw(verticalMovement) > 0) {
                MoveUp();
            } else {
                MoveDown();
            }

            return;
        }

        if(Input.GetButtonDown(selectButton)) {
            buttonsGrid[currentIndex.x, currentIndex.y].Click();
        }
    }

    private void MoveLeft() {
        currentIndex.x--;

        if(currentIndex.x < 0) {
            currentIndex.x = 1;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
    }

    private void MoveRight() {
        currentIndex.x++;

        if(currentIndex.x > 1) {
            currentIndex.x = 0;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
    }

    private void MoveUp() {
        currentIndex.y++;

        if(currentIndex.y > 2) {
            currentIndex.y = 0;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
    }

    private void MoveDown() {
        currentIndex.y--;

        if(currentIndex.y < 0) {
            currentIndex.y = 2;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
    }
}
