using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboManager : Singleton<ComboManager> {
	[Header("References")]
	[SerializeField, Tooltip("Combo panel UI element that holds combo bar and text.")] private GameObject comboPanel = default;
	[SerializeField, Tooltip("RectTransform component of combobar.")] private RectTransform comboBarTransform = default;
	[SerializeField, Tooltip("UI text to show current combo.")] private TextMeshProUGUI comboText = default;


	[Header("Attributes")]
	[SerializeField, Tooltip("Combo duration")] private float comboDuration = 10;

	[Header("Read-Only")]
	[SerializeField] private int currentCombo = 0;

	public int CurrentCombo { get => currentCombo; private set => currentCombo = value; }

	private bool comboActive;

	private float elapsedTime;
	private float step;

	private void Awake() {
		step = 1f / comboDuration;
	}

	private void Update() {
		if(comboActive) {

			elapsedTime += Time.deltaTime;

			// Count down combo bar.
			if(elapsedTime >= 1) {
				elapsedTime = 0;
				comboBarTransform.localScale = new Vector2(comboBarTransform.localScale.x - step, comboBarTransform.localScale.y);
			}


			// If combo bar reaches 0, stop combo multiplier.
			if(comboBarTransform.localScale.x <= 0) {
				currentCombo = 0;
				ToggleComboStatus(false);
			}
		}
	}

	public void IncreaseCombo() {
		// Reset combo bar.
		comboBarTransform.localScale = new Vector2(1, comboBarTransform.localScale.y);
		elapsedTime = 0;

		if(!comboActive) {
			ToggleComboStatus(true);
		}

		// Increase and update the current combo.
		currentCombo++;
		comboText.text = "X" + currentCombo;
	}

	private void ToggleComboStatus(bool state) {
		comboActive = state;
		comboPanel.SetActive(state);
	}
}
