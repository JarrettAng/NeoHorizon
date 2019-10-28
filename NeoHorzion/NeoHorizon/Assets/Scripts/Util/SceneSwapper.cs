using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : Singleton<SceneSwapper>
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

    public void ReloadCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
