using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController :MonoBehaviour
{
   //Game Controller Is a Singleton Pattern Object.
   public static GameController Instance { get; private set; }

   //Assign in Unity Inspector
   public InputHandler inputHandler;
   public DataController dataController;
   public SceneController sceneController;
   public UIController uiController;


   private void Awake()
   {
      if ( Instance == null )
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }



}
