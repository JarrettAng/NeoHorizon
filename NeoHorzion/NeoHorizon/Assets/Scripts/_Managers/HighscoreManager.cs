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
        if(index < 0 || index >= storedData.Count) return null;

        return storedData[index];
    }

    public PlayerInfo GetPlayerDataFromName(string name) {
        PlayerInfo requestedData = storedData.Find(x => x.Name == name);

        if(requestedData == null) {
            requestedData = new PlayerInfo {
                Name = name,
            };

            storedData.Add(requestedData);
        }

        return requestedData;
    }

    public void SavePlayerData() {
        SaveSystem.SaveHighscoreData(storedData);
    }
}
