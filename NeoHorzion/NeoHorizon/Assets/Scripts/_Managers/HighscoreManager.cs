using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : Singleton<HighscoreManager>
{
    [Header("Read-Only")]
    [SerializeField] private List<PlayerInfo> storedData = default;

    private void Awake() {
        List<PlayerInfo> unstoredData = SaveSystem.LoadHighscoreData();

        SortDataByScore(unstoredData);

        void SortDataByScore(List<PlayerInfo> unsortedData) {
            storedData = unsortedData.OrderByDescending(x => x.GetTotal()).ToList();
        }
    }

    /// <summary>
    /// Slots start from 0
    /// </summary>
    /// <param name="index"></param>
    public PlayerInfo GetInfoAtSlot(int index) {
        if(index == 0 || index >= storedData.Count) return null;

        return storedData[index];
    }
}
