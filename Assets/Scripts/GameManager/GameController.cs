using UnityEngine;

public class GameController : MonoBehaviour
{
    //Game Controller Is a Singleton Pattern Object.
    public static GameController Instance { get; private set; }

    //Assign in Unity Inspector
    public InputHandler inputHandler;
    public DataController dataController;
    public SceneController sceneController;
    public UIController uiController;


    //LevelDependent scripts, loaded per level, utlize an Initialization Method to call from the level controller.
    [HideInInspector] public LoadingSceneController loadingSceneController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }


    /// <summary>
    /// Use during loading scene, set to null after loading complete.
    /// </summary>
    public void InitializeLoadingSceneControllerInGameController()
    {
        GameObject loadingSceneControllerObject = GameObject.Find("LoadingSceneController");
        if (loadingSceneControllerObject == null)
        {
            loadingSceneControllerObject = new GameObject("LoadingSceneController");
            loadingSceneControllerObject.AddComponent<LoadingSceneController>();
        }
        loadingSceneController = loadingSceneControllerObject.GetComponent<LoadingSceneController>();
    }

    /// <summary>
    /// Use when done loading at scene change.
    /// </summary>
    public void SetLoadingSceneControllerToNullInGameController()
    {
        loadingSceneController = null;
    }

}
