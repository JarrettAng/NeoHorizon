using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearingManager : MonoBehaviour
{
    private GameGrid gameGrid;

    private void Awake() {
        EventManager.OnLineClear += HandleLineClear;

        gameGrid = GameGrid.Instance;
    }

    private void HandleLineClear(List<TilePiece> piecesToRemove) {
        foreach(TilePiece piece in piecesToRemove) {
            gameGrid.ClearPieceAt(piece.CurrentGridPos);
            piece.Destroy();
        }
    }
}
