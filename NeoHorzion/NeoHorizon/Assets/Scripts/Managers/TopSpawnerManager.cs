using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawnerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilePiece piecePrefab = default;

    [Header("Attributes")]
    [SerializeField] private float autoMoveSeconds = 10f;

    private GameGrid gameGrid;

    private List<TilePiece> pieces;

    private WaitForSeconds moveWaitTime;

    private int spawnHeight;
    private int spawnWidth;
    
    private void Start() {
        gameGrid = GameGrid.Instance;
        spawnHeight = gameGrid.Height - 1;
        spawnWidth = gameGrid.Width;

        moveWaitTime = new WaitForSeconds(autoMoveSeconds);

        pieces = new List<TilePiece>();

        StartCoroutine(MoveDown());
    }

    private void SpawnRow() {

        for(int x = 0; x < spawnWidth; x++) {
            bool skip = Random.value > 0.5f;

            if(skip) continue;

            Vector2Int spawnPos = new Vector2Int(x, spawnHeight);

            TilePiece newPiece = Instantiate(piecePrefab);
            newPiece.Type = PieceType.STATIC;

            gameGrid.AddPiece(spawnPos, newPiece);

            pieces.Add(newPiece);
        }
    }

    private IEnumerator MoveDown() {
        while(true) {
            SpawnRow();
            yield return moveWaitTime;
            MovePiecesDown();
        }
    }

    private void MovePiecesDown() {
        foreach(TilePiece piece in pieces) {
            gameGrid.MovePieceAt(piece.CurrentGridPos, DirectionType.DOWN, out bool moveResult);
        }
    }
}
