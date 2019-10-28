using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeaderboard : MonoBehaviour
{
    [Header("References")]
    [Header("Arrange from first to last")]
    [SerializeField] private LeaderboardSlot[] slots = default;

    [Header("Attributes")]
    [SerializeField] private int numberToLoad = 3;

    private void Start() {
        LoadHighscores(numberToLoad);
    }

    private void LoadHighscores(int numberToLoad) {
        HighscoreManager highscoreManager = HighscoreManager.Instance;

        for(int index = 0; index < numberToLoad; index++) {
            PlayerInfo currentInfo = highscoreManager.GetInfoAtSlot(index);

            if(currentInfo == null) {
                slots[index].UpdateDisplay();
            } else {
                slots[index].UpdateDisplay(currentInfo);
            }

        }
    }
}
