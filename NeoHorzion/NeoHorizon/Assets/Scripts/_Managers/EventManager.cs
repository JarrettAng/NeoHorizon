using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static Action<int> OnMoveTopDown;
    public static Action OnGameOver;
    public static Action<List<TilePiece>> OnLineClear;

    private void OnDisable() {
        ClearAllEvents();
    }

    public static void ClearAllEvents() {
        OnMoveTopDown = null;
        OnGameOver = null;
    }
}
