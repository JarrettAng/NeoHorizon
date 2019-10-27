using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComplexPiece : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected MovingPiece piecePrefab = default;

    protected Vector2 startingPos;
    protected Vector2Int startingGridPos;
    protected bool spawnedOnLeft;

    public void Initialize(Vector2Int _startingGridPos, Vector2 _startingPos, bool _spawnedOnLeft) {
        startingGridPos = _startingGridPos;
        startingPos = _startingPos;
        spawnedOnLeft = _spawnedOnLeft;

        Setup();
    }

    protected abstract void Setup();
}
