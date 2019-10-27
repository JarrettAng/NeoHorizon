using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridCell
{
    public GameGridCell(Vector2 spawnPos, Vector2Int gridPos) {
        position = spawnPos;
        gridPosition = gridPos;

        //statusSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //statusSphere.transform.position = position;

        //DebugStatus();
    }

    public TilePiece CurrentPiece;

    private Vector2Int gridPosition;
    private Vector2 position;

    private GameObject statusSphere;

    public void AddPiece(TilePiece newPiece) {
        CurrentPiece = newPiece;

        CurrentPiece.transform.position = position;
        CurrentPiece.CurrentGridPos = gridPosition;

        //DebugStatus();
    }

    public void Clear() {
        CurrentPiece = null;

        //DebugStatus();
    }

    public void DebugStatus() {
        if(CurrentPiece == null) {
            statusSphere.GetComponent<Renderer>().material.color = Color.red;
        } else {
            statusSphere.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public Vector2 GetPosition() {
        return position;
    }
}
