using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPiece : TilePiece
{
    [Header("Moving Attributes")]
    public DirectionType MoveDirection;
    [SerializeField] private float timeStep = 1.5f;

    private GameGrid gameGrid;
    private WaitForSeconds waitTime;

    private void Start() {
        gameGrid = GameGrid.Instance;

        waitTime = new WaitForSeconds(timeStep);

        StartCoroutine(Move());
    }

    private IEnumerator Move() {
        while(true) {
            yield return waitTime;
            gameGrid.MovePieceAt(CurrentGridPos, MoveDirection, out bool moveSuccess);

            if(!moveSuccess) {
                // Don't bother turning if it can't move forward because it's going out of the grid
                if(CurrentGridPos.x == 0 || CurrentGridPos.x == gameGrid.Width - 1) continue;

                if(MoveDirection == DirectionType.LEFT) {
                    MoveDirection = DirectionType.RIGHT;
                } else {
                    MoveDirection = DirectionType.LEFT;
                }

                gameGrid.MovePieceAt(CurrentGridPos, MoveDirection, out bool second);
            }
        }
    }
}
