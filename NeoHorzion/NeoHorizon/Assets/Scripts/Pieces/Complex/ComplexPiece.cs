using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComplexPiece : MovingPiece
{
    [Header("References")]
    [SerializeField] protected MovingPiece piecePrefab = default;



    public override void Initialize(Vector2Int _startingGridPos, DirectionType startingMoveDirection) {
        Setup();

        base.Initialize(_startingGridPos, startingMoveDirection);
    }

    protected abstract void Setup();
}
