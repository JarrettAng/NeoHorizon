using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePiece : MovingPiece {
	protected override void StartMoving() {
		StartCoroutine(Move());
	}

	private IEnumerator Move() {
		while(true) {
			yield return moveWaitTime;
			gameGrid.MovePieceAt(CurrentGridPos, MoveDirection, out MoveResult moveResult);

			if(!CheckSpaceAbove()) {
				break;
			}

			switch(moveResult) {
				case MoveResult.BLOCKED:
					if(MoveDirection == DirectionType.LEFT) {
						MoveDirection = DirectionType.RIGHT;
					} else {
						MoveDirection = DirectionType.LEFT;
					}

					gameGrid.MovePieceAt(CurrentGridPos, MoveDirection, out MoveResult second);
					break;

				case MoveResult.OUTOFBOUNDS:
					gameGrid.ClearPieceAt(CurrentGridPos);
					Destroy();
					break;
			}
		}
	}

	protected override void StartZoomingUp() {
		StartCoroutine(ZoomUp());
	}

	private IEnumerator ZoomUp() {
		bool shouldStopZooming = false;

		while(true) {
			if(!CheckSpaceAbove()) {
				break;
			}
			yield return zoomWaitTime;
			gameGrid.MovePieceAt(CurrentGridPos, DirectionType.UP, out MoveResult moveSuccess);

			switch(moveSuccess) {
				case MoveResult.OUTOFBOUNDS:
					shouldStopZooming = true;
					Type = PieceType.STATIC;
					TopSpawnerManager.Instance.AddMovingPiece(this);
					break;
			}

			if(shouldStopZooming) yield break;
		}
	}

	private bool CheckSpaceAbove() {
		Vector2Int gridPosAbove = new Vector2Int(CurrentGridPos.x, CurrentGridPos.y + 1);

		TilePiece blockingPiece = gameGrid.GetPieceAt(gridPosAbove);

		// All clear! Do nothing
		if(blockingPiece == null || blockingPiece.Type == PieceType.NULL) return true;

		switch(blockingPiece.Type) {
			case PieceType.STATIC:
				Type = PieceType.STATIC;
				TopSpawnerManager.Instance.AddMovingPiece(this);
                GetComponent<PieceHealth>().HandlePieceAttached();

                break;
		}

		return false;
	}
}
