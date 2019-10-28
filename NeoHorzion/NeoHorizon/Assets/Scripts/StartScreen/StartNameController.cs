using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartNameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI nameText = default;

    [Header("Read-Only")]
    [SerializeField] private string currentName;

    private void Start() {
        currentName = SaveSystem.LoadCurrentName();
        UpdateNameDisplay();
    }

    private void UpdateNameDisplay() {
        nameText.text = currentName;
    }
}
