using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ButtonPressEmulator zapSplatButton = default;
    [SerializeField] private ButtonPressEmulator freeSoundsButton = default;
    [SerializeField] private ButtonPressEmulator jarrettButton = default;
    [SerializeField] private ButtonPressEmulator junRongButton = default;
    [SerializeField] private ButtonPressEmulator backButton = default;
    [SerializeField] private RectTransform selector = default;

    [Header("Attributes")]
    [SerializeField, Tooltip("Axes for up / down movement")] private string verticalMovement = "Vertical";
    [SerializeField, Tooltip("Axes for selecting")] private string selectButton = "Shoot";
    [SerializeField] private float moveDelay = 0.2f;

    [Header("Read-Only")]
    [SerializeField] private int currentIndex;

    private ButtonPressEmulator[] buttonsList;
    private SoundManager soundManager;

    private bool readyToMove = true;

    // Bug fix (0001): Multiple calls of GetButtonDown when it's not suppose to
    private bool buttonPressedFailSafe;

    private void Awake() {
        soundManager = SoundManager.Instance;

        buttonsList = new ButtonPressEmulator[5];

        buttonsList[0] = zapSplatButton;
        buttonsList[1] = freeSoundsButton;
        buttonsList[2] = jarrettButton;
        buttonsList[3] = junRongButton;
        buttonsList[4] = backButton;
    }

    private void Start() {
        selector.anchoredPosition = buttonsList[currentIndex].Position;
    }

    private void OnEnable() {
        // Bug fix (0001)
        buttonPressedFailSafe = true;

        currentIndex = 0;
        selector.anchoredPosition = buttonsList[currentIndex].Position;
    }

    private void Update() {
        if(Input.GetButtonDown(selectButton)) {
            // Bug fix (0001)
            if(!buttonPressedFailSafe) {
                buttonPressedFailSafe = true;

                buttonsList[currentIndex].Click();
            }
        }

        // Bug fix (0001)
        if(!Input.GetButton(selectButton)) {
            if(buttonPressedFailSafe) {
                buttonPressedFailSafe = false;
            }
        }

        if(!readyToMove) return;

        if(Input.GetAxisRaw(verticalMovement) > 0) {
            MoveUp();
        } else if(Input.GetAxisRaw(verticalMovement) < 0) {
            MoveDown();
        }
    }

    public void OpenZapSplatURL() {
        Application.OpenURL("https://www.zapsplat.com");
    }

    public void OpenFreeSoundsURL() {
        Application.OpenURL("https://freesound.org");
    }

    public void OpenJarrettURL() {
        Application.OpenURL("https://jarrett-ang.itch.io");
    }

    public void OpenJunRongURL() {
        Application.OpenURL("https://soulbounded.itch.io");
    }

    private void MoveUp() {
        currentIndex--;

        if(currentIndex < 0) {
            currentIndex = buttonsList.Length - 1;
        }

        selector.anchoredPosition = buttonsList[currentIndex].Position;
        readyToMove = false;
        Invoke("ResetReadyToMove", moveDelay);
        soundManager.PlaySound("MenuScroll");
    }

    private void MoveDown() {
        currentIndex++;

        if(currentIndex > buttonsList.Length - 1) {
            currentIndex = 0;
        }

        selector.anchoredPosition = buttonsList[currentIndex].Position;
        readyToMove = false;
        Invoke("ResetReadyToMove", moveDelay);
        soundManager.PlaySound("MenuScroll");
    }

    private void ResetReadyToMove() {
        readyToMove = true;
    }
}
