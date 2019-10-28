using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardSlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI nameText = default; 
    [SerializeField] private TextMeshProUGUI totalText = default;
    [SerializeField] private TextMeshProUGUI shootText = default;
    [SerializeField] private TextMeshProUGUI ballText = default; 

    public void UpdateDisplay(PlayerInfo infoToLoad) {
        nameText.text = infoToLoad.Name;
        totalText.text = infoToLoad.GetTotal().ToString();
        shootText.text = infoToLoad.shootScore.ToString("000000");
        ballText.text = infoToLoad.ballScore.ToString("000000");
    }

    public void UpdateDisplay() {
        nameText.text = "---";
        totalText.text = "000000";
        shootText.text = "000000";
        ballText.text = "000000";
    }
}
