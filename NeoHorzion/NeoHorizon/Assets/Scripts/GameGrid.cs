using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 25;
    [SerializeField] private float cellSize = 5;


    private GameGridCell[,] grid;

    private void Awake() {
        grid = new GameGridCell[width, height];

        Vector2 topLeftPos;

        // Start spawning from top left
        if(width % 2 == 0) {
            topLeftPos.x = (cellSize * (float)(-width / 2)) + (cellSize / 2f);
        } else {
            topLeftPos.x = cellSize * Mathf.Floor(-width / 2);
        }
        if(height % 2 == 0) {
            topLeftPos.y = (cellSize * (float)(-height / 2)) + (cellSize / 2f);
        } else {
            topLeftPos.y = cellSize * Mathf.Floor(-height / 2);
        }

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                Vector2 spawnPos = new Vector2(topLeftPos.x + cellSize * x, topLeftPos.y + cellSize * y);

                GameGridCell newCell = new GameGridCell(spawnPos);
            }
        }
    }
}
