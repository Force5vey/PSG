using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public SceneData sceneData;

    public void LoadNextScene(string sceneName)
    {
      Debug.Log("SceneController > LoadNextScene > " + sceneName);

        SceneManager.LoadScene(sceneName);
    }

}
