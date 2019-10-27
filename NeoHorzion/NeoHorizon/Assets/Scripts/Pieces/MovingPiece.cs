using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingPiece : TilePiece
{
    [Header("Moving Attributes")]
    public DirectionType MoveDirection;
    [SerializeField] private float moveWaitStep = 1.5f;
    [SerializeField] private float zoomWaitStep = 0.5f;

    protected GameGrid gameGrid;
    protected WaitForSeconds moveWaitTime;
    protected WaitForSeconds zoomWaitTime;

    private void Start() {
        gameGrid = GameGrid.Instance;

        moveWaitTime = new WaitForSeconds(moveWaitStep);
        zoomWaitTime = new WaitForSeconds(zoomWaitStep);

        StartMoving();
    }

    protected abstract void StartMoving();
    protected abstract void StartZoomingUp();

    public void HandlePieceDestroyed() {
        StopAllCoroutines();

        StartZoomingUp();
    }
}
