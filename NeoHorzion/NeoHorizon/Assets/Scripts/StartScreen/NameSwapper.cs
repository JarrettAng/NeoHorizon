using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameSwapper : MonoBehaviour {
	[Header("References")]
	[SerializeField] public TextMeshProUGUI[] letters = default;
	[SerializeField] public GameObject[] selectionArrows;

	[Header("Attributes")]
	[SerializeField, Tooltip("Axes for up / down movement")] private string verticalMovement = "Vertical";
	[SerializeField, Tooltip("Axes for selecting")] private string selectButton = "Shoot";
	[SerializeField, Tooltip("Axes for selecting")] private string cancelButton = "Cancel";
	[SerializeField] private float moveDelay = 0.2f;
	[SerializeField] private Color selectedColor = Color.white;

	[Header("Read-Only")]
	[SerializeField] private string initials = "";
	[SerializeField] private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	private SoundManager soundManager;

	private int stepper = 0;
	private int letterSelect = 0;

	private bool readyToMove = true;
	private bool nameEntered = false;

    // Bug fix (0001): Multiple calls of GetButtonDown when it's not suppose to
    #if UNITY_WEBGL
    private bool buttonPressedFailSafe;
    #endif

    private void Awake() {
		soundManager = SoundManager.Instance;
	}

	private void OnEnable() {
		Reset();

		letters[letterSelect].text = alphabet[stepper].ToString();
		ToggleSelectionIndicators(letterSelect);

		void Reset() {
            // Bug fix (0001)
            readyToMove = false;
            Invoke("ResetReadyToMove", moveDelay);

            nameEntered = false;

			stepper = 0;
			letterSelect = 0;
			initials = "";

			foreach(TextMeshProUGUI letter in letters) {
				letter.color = Color.white;
				letter.text = alphabet[stepper].ToString();
			}
		}
	}

	private void Update() {
        // Bug fix (0001)
        #if UNITY_WEBGL
        if(!Input.GetButton(selectButton)) {
            if(buttonPressedFailSafe) {
                buttonPressedFailSafe = false;
            }
        }
        #endif

        if(Input.GetButtonDown(cancelButton)) {
			StartScreenSwapper.Instance.OpenMainPanel();
			soundManager.PlaySound("UIBack");
		}

		if(nameEntered) return;

		if(Input.GetAxisRaw(verticalMovement) > 0 && readyToMove) {
			stepper++;

			if(stepper >= alphabet.Length) {
				stepper = 0;
			}

			soundManager.PlaySound("MenuScroll");

			letters[letterSelect].text = alphabet[stepper].ToString();
			readyToMove = false;
			Invoke("ResetReadyToMove", moveDelay);

		} else if(Input.GetAxisRaw(verticalMovement) < 0 && readyToMove) {
			stepper--;

			if(stepper < 0) {
				stepper = alphabet.Length - 1;
			}

			soundManager.PlaySound("MenuScroll");

			letters[letterSelect].text = alphabet[stepper].ToString();
			readyToMove = false;
			Invoke("ResetReadyToMove", moveDelay);
		}

		if(Input.GetButtonDown(selectButton)) {
            #if UNITY_WEBGL
            if(buttonPressedFailSafe) {
                return;
            }

            buttonPressedFailSafe = true;
            #endif

            if(letterSelect <= letters.Length - 1) {
				initials = initials + alphabet[stepper].ToString(); // add current letter to string
																	// if the last letter is reached then add initials
				if(letterSelect >= letters.Length - 1) {
					letters[letterSelect].color = selectedColor;
					nameEntered = true;

					SaveSystem.SaveCurrentName(initials);

					StartScreenSwapper.Instance.OpenMainPanel();

				} else {
					letterSelect++;
					letters[letterSelect - 1].color = selectedColor;
					ToggleSelectionIndicators(letterSelect);
					readyToMove = false;
					Invoke("ResetReadyToMove", moveDelay);
				}

				soundManager.PlaySound("UISelect");

				stepper = 0; // stepper is reset for next run
			}
		}
	}

	private void ResetReadyToMove() {
		readyToMove = true;
	}

	private void ToggleSelectionIndicators(int index) {
		for(int i = 0; i < selectionArrows.Length; i++) {
			if(i == index) {
				selectionArrows[i].SetActive(true);
			} else {
				selectionArrows[i].SetActive(false);
			}
		}
	}
}
