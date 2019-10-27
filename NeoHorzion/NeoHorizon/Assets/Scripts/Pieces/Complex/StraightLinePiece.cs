using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLinePiece : ComplexPiece
{
    private MovingPiece[,] positions;

    protected override void Setup() {
        //positions = new MovingPiece[3, 1];

        //positions[0, 0] = Instantiate(piecePrefab);
        //positions[1, 0] = Instantiate(piecePrefab);
        //positions[2, 0] = Instantiate(piecePrefab);

        //Vector2 currentPos;
        //float cellSize = GameGrid.Instance.CellSize;

        //if(spawnedOnLeft) {
        //    positions[2, 0].transform.position = startingPos;

        //    currentPos = startingPos;
        //    currentPos.x -= cellSize;
        //    positions[1, 0].transform.position = currentPos;

        //    currentPos = startingPos;
        //    currentPos.x -= cellSize * 2;
        //    positions[0, 0].transform.position = currentPos;
        //} else {
        //    positions[0, 0].transform.position = startingPos;

        //    currentPos = startingPos;
        //    currentPos.x += cellSize;
        //    positions[1, 0].transform.position = currentPos;

        //    currentPos = startingPos;
        //    currentPos.x += cellSize * 2;
        //    positions[2, 0].transform.position = currentPos;
        //}
    }

    protected override void StartMoving() {
        throw new System.NotImplementedException();
    }

    protected override void StartZoomingUp() {
        throw new System.NotImplementedException();
    }
}
