using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpawnerManager : Singleton<TopSpawnerManager>
{
    [Header("References")]
    [SerializeField] private TilePiece piecePrefab = default;

    [Header("Attributes")]
    [SerializeField] private int gapsPerRow = 1;
    [SerializeField, Tooltip("Use this to prevent gaps spawning at the side")] private int gapSideBuffer = 1;
    [SerializeField] private float autoMoveSeconds = 10f;

    [Header("Read-Only")]
    [SerializeField] private int freeHeight;

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

    public void AddMovingPiece(TilePiece movingPiece) {
        // Why insert at index 0 you ask? Well, the system to move pieces down is run in a foreach loop.
        // Meaning the pieces at the bottom must move down first before the pieces at the top (otherwise they won't move)
        // And this moving piece that just got added will always move first (so it doesn't block other pieces)
        pieces.Insert(0, movingPiece);

        CheckPieceClearedLine(movingPiece.CurrentGridPos);
        //EventManager.OnPieceAdded?.Invoke(pieces);
    }

    private void CheckPieceClearedLine(Vector2Int piecePos) {
        bool lineFull = true;

        Vector2Int gridCheckPos = new Vector2Int(0, piecePos.y);
        for(int xPos = 0; xPos < spawnWidth; xPos++) {
            gridCheckPos.x = xPos;

            if(gameGrid.IsTileEmpty(gridCheckPos)) {
                lineFull = false;
                break;
            }
        }

        if(lineFull) {
            List<TilePiece> piecesToRemove = new List<TilePiece>();

            foreach(TilePiece piece in pieces) {
                if(piece.CurrentGridPos.y <= piecePos.y) {
                    piecesToRemove.Add(piece);
                }
            }

            foreach(TilePiece piece in piecesToRemove) {
                pieces.Remove(piece);
            }

            EventManager.OnLineClear?.Invoke(piecesToRemove);

            if(pieces.Count <= 0) {
                Invoke("RepopulateGrid", 0.25f);
            }
        }
    }

    private void RepopulateGrid() {
        MovePiecesDown();
        SpawnRow();
    }

    private IEnumerator MoveDown() {
        while(true) {
            SpawnRow();
            EventManager.OnMoveTopDown?.Invoke(freeHeight);
            yield return moveWaitTime;
            MovePiecesDown();
        }
    }

    private void SpawnRow() {
        List<int> emptySpots = new List<int>();

        int maxIndex = spawnWidth - gapSideBuffer;
        for(int index = gapSideBuffer; index < maxIndex; index++) {
            emptySpots.Add(index);
        }

        for(int count = maxIndex - 1; count > gapsPerRow; count--) {
            int randomIndex = Random.Range(0, emptySpots.Count);
            emptySpots.RemoveAt(randomIndex);
        }

        for(int x = 0; x < spawnWidth; x++) {
            bool skip = false;

            foreach(int emptySpot in emptySpots) {
                if(emptySpot == x) {
                    skip = true;
                    break;
                }
            }

            if(skip) continue;

            Vector2Int spawnPos = new Vector2Int(x, spawnHeight);

            TilePiece newPiece = Instantiate(piecePrefab);
            newPiece.Type = PieceType.STATIC;

            gameGrid.AddPieceAt(spawnPos, newPiece, out bool addWasSuccessful);

            if(!addWasSuccessful) {
                newPiece.Destroy();
            }

            pieces.Add(newPiece);
        }

        UpdateFreeHeight();
    }

    private void UpdateFreeHeight() {
        for(int index = gameGrid.Height - 1; index > 0; index--) {
            if(gameGrid.IsTileEmpty(new Vector2Int(0, index))) {
                freeHeight = index;
                break;
            }
        }
    }

    private void MovePiecesDown() {
        foreach(TilePiece piece in pieces) {
            gameGrid.MovePieceAt(piece.CurrentGridPos, DirectionType.DOWN, out MoveResult moveResult);
        }
    }
}
