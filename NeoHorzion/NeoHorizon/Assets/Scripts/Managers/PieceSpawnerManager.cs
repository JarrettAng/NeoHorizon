using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawnerManager : MonoBehaviour
{
    private GameGrid gameGrid;

    private int searchHeight;

    private void Awake() {
        gameGrid = GameGrid.Instance;

        searchHeight = gameGrid.Height - 1;
    }


}
