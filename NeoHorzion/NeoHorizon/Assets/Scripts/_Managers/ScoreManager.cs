using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
	[Header("References")]
	[SerializeField] private TextMeshProUGUI highscoreText = default;
	[SerializeField] private TextMeshProUGUI scoreText = default;
	[SerializeField] private TextMeshProUGUI linesText = default;

	[Header("Attributes")]
	[SerializeField] private int pointsPerBlock = 50;

	[Header("Read-only")]
	[SerializeField] private int highscore;
	[SerializeField] private int currentScore;
	[SerializeField] private int linesCleared;

	private void Awake() {
		EventManager.OnLineClear += UpdateScore;
	}

	private void Start() {
		LoadValues();

		UpdateHighscore();
		UpdateScore();
        UpdateLines();

        void LoadValues() {
			highscore = PlayerPrefs.GetInt("Highscore", 0);
			currentScore = 0;
            linesCleared = 0;
        }
	}

	private void UpdateScore(List<TilePiece> piecesRemoved) {
		currentScore += piecesRemoved.Count * pointsPerBlock;
        linesCleared++;

        UpdateHighscore();
		UpdateScore();
        UpdateLines();
    }

	private void UpdateHighscore() {
		if(currentScore > highscore) {
			highscore = currentScore;
			PlayerPrefs.SetInt("Highscore", highscore);
		}

		highscoreText.text = highscore.ToString("000000");
	}

	private void UpdateScore() {
		scoreText.text = currentScore.ToString("000000");
	}

    private void UpdateLines() {
        linesText.text = linesCleared.ToString("000000");
    }
}
