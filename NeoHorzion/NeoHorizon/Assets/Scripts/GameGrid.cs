﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : Singleton<GameGrid>
{
    [Header("Attributes")]
    public int Width = 10;
    public int Height = 18;
    public float CellSize = 5;


    private GameGridCell[,] grid;

    private void Awake() {
        grid = new GameGridCell[Width, Height];

        Vector2 topLeftPos;

        // Start spawning from top left
        if(Width % 2 == 0) {
            topLeftPos.x = (CellSize * (float)(-Width / 2)) + (CellSize / 2f);
        } else {
            topLeftPos.x = CellSize * Mathf.Floor(-Width / 2);
        }
        if(Height % 2 == 0) {
            topLeftPos.y = (CellSize * (float)(-Height / 2)) + (CellSize / 2f);
        } else {
            topLeftPos.y = CellSize * Mathf.Floor(-Height / 2);
        }

        for(int y = 0; y < Height; y++) {
            for(int x = 0; x < Width; x++) {
                Vector2 spawnPos = new Vector2(topLeftPos.x + CellSize * x, topLeftPos.y + CellSize * y);

                GameGridCell newCell = new GameGridCell(spawnPos, new Vector2Int(x, y));
                grid[x, y] = newCell;
            }
        }
    }

    public void AddPieceAt(Vector2Int gridPos, TilePiece newPiece, out bool addWasSuccessful) {
        if(IsTileEmpty(gridPos)) {
            grid[gridPos.x, gridPos.y].AddPiece(newPiece);
            addWasSuccessful = true;
        } else {
            addWasSuccessful = false;
        }
    }

    public bool IsTileEmpty(Vector2Int gridPos) {
        return grid[gridPos.x, gridPos.y].CurrentPiece == null;
    }

    public void MovePieceAt(Vector2Int gridPos, DirectionType moveDirection, out MoveResult moveResult) {

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
            moveResult = MoveResult.OUTOFBOUNDS;
        } else {
            if(IsTileEmpty(newPos)) {
                TilePiece piece = grid[gridPos.x, gridPos.y].CurrentPiece;
                grid[gridPos.x, gridPos.y].Clear();

                grid[newPos.x, newPos.y].AddPiece(piece);

                moveResult = MoveResult.SUCCESS;
            } else {
                moveResult = MoveResult.BLOCKED;
            }
        }


        bool IsOutOfBounds(Vector2Int pos) {
            bool outOfBounds = false;

            if(pos.x < 0 || pos.x > Width - 1) outOfBounds = true;
            if(pos.y < 0 || pos.y > Height - 1) outOfBounds = true;

            return outOfBounds;
        }
    }

    public TilePiece GetPieceAt(Vector2Int gridPos) {
        if(gridPos.x < 0 || gridPos.x > Width - 1 || gridPos.y < 0 || gridPos.y > Height - 1) {
            return null;
        }

        return grid[gridPos.x, gridPos.y].CurrentPiece;
    }

    public void ClearPieceAt(Vector2Int gridPos) {
        if(gridPos.x < 0 || gridPos.x > Width - 1 || gridPos.y < 0 || gridPos.y > Height - 1) {
            Debug.LogWarningFormat("Requested clearing at invalid grid position: {0}", gridPos);
            return;
        }

        grid[gridPos.x, gridPos.y].Clear();
    }
}
