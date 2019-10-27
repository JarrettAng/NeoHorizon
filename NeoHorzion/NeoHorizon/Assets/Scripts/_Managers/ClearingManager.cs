using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearingManager : MonoBehaviour {
	private SoundManager soundManager;

	private GameGrid gameGrid;

	private void Awake() {
		EventManager.OnLineClear += HandleLineClear;

		soundManager = SoundManager.Instance;

		gameGrid = GameGrid.Instance;
	}

	private void HandleLineClear(List<TilePiece> piecesToRemove) {
		soundManager.PlaySound("RowClear");

		foreach(TilePiece piece in piecesToRemove) {
			gameGrid.ClearPieceAt(piece.CurrentGridPos);
			piece.Cleared();
		}
	}
}
