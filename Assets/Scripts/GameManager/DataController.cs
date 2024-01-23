using System;
using System.IO;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

public class DataController : MonoBehaviour
{
   [Header("Game Data & Paths")]
   public GameData gameData;

   private string saveGameDataPath;
   private string saveGameDataPathBackup;

   [Header("Events")]
   public UnityEvent onLoadingError;

   public UnityEvent onGameDataLoadedSuccessfully;
   public UnityEvent onGameDataSavedSuccessfully;
   public UnityEvent onGameDataSaveError;

   private void Awake()
   {
      InitializeEvents();
      InitializePaths();

      //Always first create a new gameData instance, this uses default constructor.
      //if game data loads it will overwrite to saved data info.
      gameData = new GameData();
   }

   private void InitializeEvents()
   {
      if (onLoadingError == null) { onLoadingError = new UnityEvent(); }
      if (onGameDataLoadedSuccessfully == null) { onGameDataLoadedSuccessfully = new UnityEvent(); }
      if (onGameDataSavedSuccessfully == null) { onGameDataSavedSuccessfully = new UnityEvent(); }
      if (onGameDataSaveError == null) { onGameDataSaveError = new UnityEvent(); }
   }

   private void InitializePaths()
   {
      saveGameDataPath = Path.Combine(Application.dataPath, "PlayerSaveFile.json");

#if UNITY_EDITOR || DEVELOPMENT_BUILD
      saveGameDataPathBackup = string.Empty; // Disable backup in development
#else
    saveGameDataPathBackup = Path.Combine(Application.persistentDataPath, "PlayerSaveFileBackup.json");
#endif
   }

   #region //Game Data Methods

   public async void LoadGameData()
   {
      gameData = await TryLoadGameDataAsync(saveGameDataPath) ?? await TryLoadGameDataAsync(saveGameDataPathBackup);

      if (gameData != null)
      {
         await SaveGameDataToDiskAsync(false); // Save backup if primary failed but backup succeeded
         UpdatePlayerData_With_GameData();
         onGameDataLoadedSuccessfully.Invoke();
      }
      else
      {
         gameData = new GameData(); // Use default data
         await SaveGameDataToDiskAsync(false); // Save default data
         onLoadingError?.Invoke();
      }
   }

   /// <summary>
   /// Try's to Load Game Data From Disk
   /// </summary>
   /// <param name="path">Save Game File</param>
   /// <param name="data">A reference to the GameData</param>
   /// <returns>True if successful</returns>
   private async Task<GameData> TryLoadGameDataAsync(string path)
   {
      if (!string.IsNullOrEmpty(path) && File.Exists(path))
      {
         try
         {
            if (File.Exists(path))
            {
               string jsonData = await File.ReadAllTextAsync(path);
               return JsonUtility.FromJson<GameData>(jsonData);
            }
         }
         catch (Exception ex)
         {
            Debug.LogError("Failed to read from file: " + path + ". Error: " + ex.Message);
         }
      }
      return null;
   }

   /// <summary>
   /// Saves the current state of GameData class to disk.
   /// Remember to Update Game Data with Player Data for most up-to-date save.
   /// </summary>
   public async Task SaveGameDataToDiskAsync(bool updateData)
   {
      if (updateData)
      {
         UpdateGameData_With_PlayerData();
      }

      string jsonData = JsonUtility.ToJson(gameData);

      bool primarySuccess = await TryWriteGameDataToDiskAsync(saveGameDataPath, jsonData);
      bool backupSuccess = true; // Assuming backup is optional
      if (!string.IsNullOrEmpty(saveGameDataPathBackup))
      {
         backupSuccess = await TryWriteGameDataToDiskAsync(saveGameDataPathBackup, jsonData);
      }

      if (!primarySuccess || !backupSuccess)
      {
         onGameDataSaveError.Invoke();
      }
      else { onGameDataSavedSuccessfully.Invoke(); }
   }

   /// <summary>
   /// Try's to write the GameData to Disk
   /// </summary>
   /// <param name="path">SavedGame File</param>
   /// <param name="data">Json formatted String</param>
   /// <returns></returns>
   private async Task<bool> TryWriteGameDataToDiskAsync(string path, string data)
   {
      try
      {
         await File.WriteAllTextAsync(path, data);
         return true;
      }
      catch (Exception ex)
      {
         Debug.LogError("Failed to write to file: " + path + ". Error: " + ex.Message);
         return false;
      }
   }

   /// <summary>
   /// Pulls current player data from active classes and updates GameData Class
   /// </summary>
   private void UpdateGameData_With_PlayerData()
   {
      //TODO: Map from active player classes.
   }

   /// <summary>
   /// Pulls currently loaded Game Data and updates active player data.
   /// </summary>
   public void UpdatePlayerData_With_GameData()
   {
      //TODO: Ensure this mapping mirrors active classes.

      PlayerController.Instance.pilotController.selectedPilotIndex = gameData.selectedPilotIndex;
   }

   #endregion //Game Data Methods
}