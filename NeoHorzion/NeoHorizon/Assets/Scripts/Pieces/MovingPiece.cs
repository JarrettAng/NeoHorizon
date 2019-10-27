using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPiece : TilePiece
{
    [Header("Moving Attributes")]
    public DirectionType MoveDirection;
    [SerializeField] private float moveWaitStep = 1.5f;
    [SerializeField] private float zoomWaitStep = 0.5f;

    private GameGrid gameGrid;
    private WaitForSeconds moveWaitTime;
    private WaitForSeconds zoomWaitTime;

    private void Start() {
        gameGrid = GameGrid.Instance;

        moveWaitTime = new WaitForSeconds(moveWaitStep);
        zoomWaitTime = new WaitForSeconds(zoomWaitStep);

        StartCoroutine(Move());
    }

    public void HandlePieceDestroyed() {
        StopAllCoroutines();

        StartCoroutine(ZoomUp());
    }

    private IEnumerator Move() {
        while(true) {
            yield return moveWaitTime;
            gameGrid.MovePieceAt(CurrentGridPos, MoveDirection, out MoveResult moveResult);

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
                    Destroy();
                    break;
            }
        }
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
                    break;
            }

            if(shouldStopZooming) break;
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
                break;

            case PieceType.MOVING:
                // Decide what happens to this later
                break;

            default:
                Debug.LogErrorFormat("Piece zooming out just collided with a piece of unknown type: {0}", blockingPiece.Type);
                break;
        }

        return false;
    }
}
