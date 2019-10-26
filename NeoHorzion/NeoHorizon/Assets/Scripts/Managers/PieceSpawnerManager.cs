using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawnerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovingPiece movingPiecePrefab;

    [Header("Attributes")]
    [SerializeField] private int searchHeight;
    [SerializeField] private float spawnFrequency = 2f;

    private GameGrid gameGrid;
    private WaitForSeconds waitTime;

    private int rightSpawnIndex;

    private void Awake() {
        gameGrid = GameGrid.Instance;
        waitTime = new WaitForSeconds(spawnFrequency);

        rightSpawnIndex = gameGrid.Width - 1;
    }

    private void Start() {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() {
        while(true) {
            UpdateHeight();
            SpawnPiece();
            yield return waitTime;
        }
    }

    private void SpawnPiece() {
        int xPos;
        int yPos = Random.Range(0, searchHeight);
        bool spawnLeft = Random.value > 0.5f;
        DirectionType moveDirection;

        if(spawnLeft) {
            xPos = 0;
            moveDirection = DirectionType.RIGHT;
        } else {
            xPos = rightSpawnIndex;
            moveDirection = DirectionType.LEFT;
        }

        Vector2Int gridSpawnPos = new Vector2Int(xPos, yPos);

        bool canSpawn = gameGrid.IsTileEmpty(gridSpawnPos);
        if(!canSpawn) {
            if(searchHeight > 3) {
                SpawnPiece();
            }
            return;
        }

        MovingPiece newPiece = Instantiate(movingPiecePrefab);
        newPiece.MoveDirection = moveDirection;

        gameGrid.AddPiece(gridSpawnPos, newPiece);
    }

    private void UpdateHeight() {
        for(int index = gameGrid.Height - 1; index > 0; index--) {
            if(gameGrid.IsTileEmpty(new Vector2Int(0, index))) {
                searchHeight = index - 2;
                break;
            }
        }
    }
}
