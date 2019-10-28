using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressEmulator : MonoBehaviour
{
    [Header("Read-Only")]
    public Vector3 Position;

    private Button button;

    private void Awake() {
        button = GetComponent<Button>();

        Position = GetComponent<RectTransform>().anchoredPosition;
    }

    public void Click() {
        button.onClick.Invoke();
    }
}
