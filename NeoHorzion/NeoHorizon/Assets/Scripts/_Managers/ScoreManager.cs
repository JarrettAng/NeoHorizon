using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
	[Header("References")]
	[SerializeField] private TextMeshProUGUI nameText = default;
	[SerializeField] private TextMeshProUGUI highscoreText = default;
	[SerializeField] private TextMeshProUGUI scoreText = default;
	[SerializeField] private TextMeshProUGUI linesText = default;

	[Header("Attributes")]
	[SerializeField] private string ballORshoot = "shoot";
	[SerializeField] private int pointsPerBlock = 50;

	[Header("Read-only")]
	[SerializeField] private PlayerInfo currentData; // Reference type, updates highscore automatically
	[SerializeField] private int currentScore = 0;
	[SerializeField] private int linesCleared = 0;

	private HighscoreManager highscoreManager;
	private ComboManager comboManager;

	private void Awake() {
		EventManager.OnLineClear += UpdateScore;

		highscoreManager = HighscoreManager.Instance;
		comboManager = ComboManager.Instance;
	}

	private void Start() {
		string name = SaveSystem.LoadCurrentName();
		currentData = highscoreManager.GetPlayerDataFromName(name);

		nameText.text = name;

		UpdateHighscore();
		UpdateScore();
		UpdateLines();
	}

	private void UpdateScore(List<TilePiece> piecesRemoved) {
		comboManager.IncreaseCombo();
		currentScore += piecesRemoved.Count * pointsPerBlock * comboManager.CurrentCombo;
		linesCleared++;

		UpdateHighscore();
		UpdateScore();
		UpdateLines();
	}

	private void UpdateHighscore() {
		if(ballORshoot == "shoot") {
			if(currentScore > currentData.shootScore) {
				currentData.shootScore = currentScore;

				highscoreManager.SavePlayerData();
			}

			highscoreText.text = currentData.shootScore.ToString("000000");

		} else if(ballORshoot == "ball") {
			if(currentScore > currentData.ballScore) {
				currentData.ballScore = currentScore;

				highscoreManager.SavePlayerData();
			}

			highscoreText.text = currentData.ballScore.ToString("000000");
		} else {
			Debug.LogErrorFormat("Invalid gamemode for ScoreManager. Given: {0}, accepted only ball or shoot", ballORshoot);
		}
	}

	private void UpdateScore() {
		scoreText.text = currentScore.ToString("000000");
	}

	private void UpdateLines() {
		linesText.text = linesCleared.ToString("000000");
	}
}
