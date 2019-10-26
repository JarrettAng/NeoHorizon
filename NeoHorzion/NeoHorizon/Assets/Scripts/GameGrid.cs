using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : Singleton<GameGrid>
{
    [Header("Attributes")]
    public int Width = 10;
    public int Height = 18;
    [SerializeField] private float cellSize = 5;


    private GameGridCell[,] grid;

    private void Awake() {
        grid = new GameGridCell[Width, Height];

        Vector2 topLeftPos;

        // Start spawning from top left
        if(Width % 2 == 0) {
            topLeftPos.x = (cellSize * (float)(-Width / 2)) + (cellSize / 2f);
        } else {
            topLeftPos.x = cellSize * Mathf.Floor(-Width / 2);
        }
        if(Height % 2 == 0) {
            topLeftPos.y = (cellSize * (float)(-Height / 2)) + (cellSize / 2f);
        } else {
            topLeftPos.y = cellSize * Mathf.Floor(-Height / 2);
        }

        for(int y = 0; y < Height; y++) {
            for(int x = 0; x < Width; x++) {
                Vector2 spawnPos = new Vector2(topLeftPos.x + cellSize * x, topLeftPos.y + cellSize * y);

                GameGridCell newCell = new GameGridCell(spawnPos, new Vector2Int(x, y));
                grid[x, y] = newCell;
            }
        }
    }

    public void AddPiece(Vector2Int gridPos, TilePiece newPiece) {
        if(IsTileEmpty(gridPos)) {
            grid[gridPos.x, gridPos.y].AddPiece(newPiece);
        } else {
            Debug.LogErrorFormat("Tried to place tile at {0} even though it already contains a piece!");
        }
    }

    public bool IsTileEmpty(Vector2Int gridPos) {
        return grid[gridPos.x, gridPos.y].CurrentPiece == null;
    }

    public void MovePieceAt(Vector2Int gridPos, DirectionType moveDirection, out bool moveResult) {

        Vector2Int newPos = new Vector2Int(gridPos.x, gridPos.y);

        switch(moveDirection) {
            case DirectionType.UP:
                newPos.y++;
                break;

            case DirectionType.DOWN:
                newPos.y--;
                break;

            case DirectionType.LEFT:
                newPos.x--;
                break;

            case DirectionType.RIGHT:
                newPos.x++;
                break;

            default:
                Debug.LogErrorFormat("Piece requested to move an unknown direction: {0}!", moveDirection);
                break;
        }

        if(IsOutOfBounds(newPos)) {
            TilePiece piece = grid[newPos.x, newPos.y].CurrentPiece;
            piece.Destroy();

            grid[newPos.x, newPos.y].Clear();
            moveResult = false;
        } else {
            if(IsTileEmpty(newPos)) {
                TilePiece piece = grid[gridPos.x, gridPos.y].CurrentPiece;
                grid[gridPos.x, gridPos.y].Clear();

                grid[newPos.x, newPos.y].AddPiece(piece);

                moveResult = true;
            } else {
                Debug.LogWarning("Tile not clear! Can't Move!!");
                moveResult = false;
            }
        }


        bool IsOutOfBounds(Vector2Int pos) {
            bool outOfBounds = false;

            if(pos.x < 0 || pos.x > Width) outOfBounds = true;
            if(pos.y < 0 || pos.y > Height) outOfBounds = true;

            return outOfBounds;
        }
    }
}
