using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridCell
{
    public GameGridCell(Vector2 spawnPos) {
        position = spawnPos;

        statusSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        statusSphere.transform.position = position;

        DebugStatus();
    }

    public TilePiece CurrentPiece;

    private Vector2 position;

    private GameObject statusSphere;

    public void DebugStatus() {
        if(CurrentPiece == null) {
            statusSphere.GetComponent<Renderer>().material.color = Color.red;
        } else {
            statusSphere.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
