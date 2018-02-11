using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public void SwitchScene()
    {
        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        Debug.Log(SceneManager.GetSceneAt(nextLevel).name);
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene(SceneManager.GetSceneAt(nextLevel).name);
    }
}
