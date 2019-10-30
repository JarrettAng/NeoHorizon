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
    [SerializeField] private float moveDelay = 0.2f;

    [Header("Read-Only")]
    [SerializeField] private Vector2Int currentIndex;

    private ButtonPressEmulator[,] buttonsGrid;
    private SoundManager soundManager;

    private bool readyToMove = true;

    // Bug fix (0001): Multiple calls of GetButtonDown when it's not suppose to
    private bool buttonPressedFailSafe;
    
    // PSA: This entire script is hardcoded! Soz

    private void Awake() {
        soundManager = SoundManager.Instance;

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
        if(Input.GetButtonDown(selectButton)) {
            // Bug fix (0001)
            if(!buttonPressedFailSafe) {
                buttonPressedFailSafe = true;

                buttonsGrid[currentIndex.x, currentIndex.y].Click();
            }
        }

        // Bug fix (0001)
        if(!Input.GetButton(selectButton)) {
            if(buttonPressedFailSafe) {
                buttonPressedFailSafe = false;
            }
        }

        if(!readyToMove) return;

        if(Input.GetAxisRaw(horizontalMovement) > 0) {
            MoveRight();
        } else if (Input.GetAxisRaw(horizontalMovement) < 0) {
            MoveLeft();
        }

        if(Input.GetAxisRaw(verticalMovement) > 0) {
            MoveUp();
        } else if(Input.GetAxisRaw(verticalMovement) < 0) {
            MoveDown();
        }
    }

    private void MoveLeft() {
        currentIndex.x--;

        if(currentIndex.x < 0) {
            currentIndex.x = 1;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
        readyToMove = false;
        Invoke("ResetReadyToMove", moveDelay);
        soundManager.PlaySound("MenuScroll");
    }

    private void MoveRight() {
        currentIndex.x++;

        if(currentIndex.x > 1) {
            currentIndex.x = 0;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
        readyToMove = false;
        Invoke("ResetReadyToMove", moveDelay);
        soundManager.PlaySound("MenuScroll");
    }

    private void MoveUp() {
        currentIndex.y++;

        if(currentIndex.y > 2) {
            currentIndex.y = 0;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
        readyToMove = false;
        Invoke("ResetReadyToMove", moveDelay);
        soundManager.PlaySound("MenuScroll");
    }

    private void MoveDown() {
        currentIndex.y--;

        if(currentIndex.y < 0) {
            currentIndex.y = 2;
        }

        selector.anchoredPosition = buttonsGrid[currentIndex.x, currentIndex.y].Position;
        readyToMove = false;
        Invoke("ResetReadyToMove", moveDelay);
        soundManager.PlaySound("MenuScroll");
    }

    private void ResetReadyToMove() {
        readyToMove = true;
    }
}
