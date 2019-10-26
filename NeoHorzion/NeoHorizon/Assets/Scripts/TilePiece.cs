using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
    public PieceType Type;

    public Vector2Int CurrentGridPos;

    public void Destroy() {
        Debug.Log("Piece destroyed!");

        Destroy(gameObject);
    }
}
