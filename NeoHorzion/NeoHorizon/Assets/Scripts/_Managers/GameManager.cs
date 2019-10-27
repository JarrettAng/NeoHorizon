using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int heightBeforeLosing = 2;

    private void Awake() {
        EventManager.OnMoveTopDown += CheckGameOver;
    }

    private void CheckGameOver(int freeHeight) {
        if(freeHeight < heightBeforeLosing) {
            EventManager.OnGameOver?.Invoke();
        }
    }
}
