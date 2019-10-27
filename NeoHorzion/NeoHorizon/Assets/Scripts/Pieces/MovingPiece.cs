using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingPiece : TilePiece {
	[Header("Moving Attributes")]
	[SerializeField] private float moveWaitStep = 1.5f;
	[SerializeField] private float zoomWaitStep = 0.5f;

	[Header("Read-Only")]
	[SerializeField] protected Vector2Int startingGridPos;
	[SerializeField] protected DirectionType MoveDirection;

	protected GameGrid gameGrid;
	protected WaitForSeconds moveWaitTime;
	protected WaitForSeconds zoomWaitTime;


	public virtual void Initialize(Vector2Int _startingGridPos, DirectionType startingMoveDirection) {
		startingGridPos = _startingGridPos;
		MoveDirection = startingMoveDirection;

		gameGrid = GameGrid.Instance;

		moveWaitTime = new WaitForSeconds(moveWaitStep);
		zoomWaitTime = new WaitForSeconds(zoomWaitStep);

		gameGrid.AddPieceAt(startingGridPos, this, out bool addWasSuccessful);

		StartMoving();
	}
	protected abstract void StartMoving();
	protected abstract void StartZoomingUp();

	public void HandlePieceDestroyed() {
		StopAllCoroutines();

		StartZoomingUp();
	}
}
