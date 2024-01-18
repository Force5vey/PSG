using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData", order = 3)]
public class SceneData : ScriptableObject
{
    [System.Serializable]
    public struct SceneInfo
    {
        public int sceneIndex;
        public string sceneName;
    }

    public List<SceneInfo> scenes = new List<SceneInfo>();


    public string GetSceneNameByIndex(int index)
    {
        foreach(SceneInfo sceneInfo in scenes)
        {
            if(sceneInfo.sceneIndex == index)
            {
                return sceneInfo.sceneName;
            }
        }
        return null;
    }


    /// <summary>
    /// Use Scene Name to retrieve the index.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>-1 if name not found.</returns>
    public int GetSceneIndexByName(string name)
    {
        foreach (SceneInfo sceneInfo in scenes)
        {
            if (sceneInfo.sceneName == name)
            {
                return sceneInfo.sceneIndex;
            }
        }

        return -1;
    }

    public string[] GetSceneNames()
    {
        return scenes.ConvertAll(scene => scene.sceneName).ToArray();
    }



}
