using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private string startSceneName = "Start";
    [SerializeField] private string shooterSceneName = "ShooterMain";

    public void LoadStartScene() {
        SceneManager.LoadScene(startSceneName);
    }

    public void LoadShootScene() {
        SceneManager.LoadScene(shooterSceneName);
    }
}
