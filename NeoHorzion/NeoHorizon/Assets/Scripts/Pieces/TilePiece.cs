using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
    [Header("Attributes")]
    public PieceType Type;
    public Vector2Int CurrentGridPos;

    public void Destroy() {
        Destroy(gameObject);
    }
}
