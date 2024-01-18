using System;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //GameData
    public GameData gameData;

    private string saveGameDataPath;
    private string saveGameDataPathBackup;

    //Loading Flags
    public enum LoadingStatus
    {
        Loading,
        Success,
        Failed
    }

    public DataController.LoadingStatus gameDataLoadStatus;
    public bool waitingGameDataAcknowledgement = false;


    private void Awake()
    {
        saveGameDataPath = Path.Combine(Application.dataPath, "PlayerSaveFile.json");
        //TempBack up during testing.
        //TODO: Change this back for builds / deploymnet to: persistentDataPath
        //saveGameDataPathBackup = Path.Combine(Application.dataPath, "PlayerSaveFileBackup.json");
        //saveGameDataPathBackup = Path.Combine(Application.persistentDataPath, "PlayerSaveFileBackup.json");

        //Always create a new one with the default constructor first, update with save data if there is one.
        gameData = new GameData();

        //Initialzie LoadStatus to default of loading, ensures all cases are hit.
        gameDataLoadStatus = LoadingStatus.Loading;
    }


    #region //Game Data Methods

    /// <summary>
    /// Pulls current player data from active classes and updates GameData Class
    /// </summary>
    public void UpdateSaveGameDataWithPlayerData()
    {

        //TODO: Map from real active player classes.
        

    }


    /// <summary>
    /// Pulls currently loaded Game Data and updates active player data.
    /// </summary>
    public void UpdatePlayerDataWithSavedGameData()
    {
        //TODO: Ensure this mapping mirrors active classes.
        
    }

    /// <summary>
    /// Load SavedGameData from disk.
    /// </summary>
    public void LoadGameData()
    {
        if (TryLoadGameData(saveGameDataPath, out gameData))
        {
            gameDataLoadStatus = LoadingStatus.Success;
            waitingGameDataAcknowledgement = false;
        }
        else if (TryLoadGameData(saveGameDataPathBackup, out gameData))
        {
            // Backup data loaded succesfully. Trigger a rewrite of the primary data file
            SaveGameDataToDisk();

            gameDataLoadStatus = LoadingStatus.Success;
            waitingGameDataAcknowledgement = false;
        }
        else
        {
            //Neither primary or back up data could be loaded, use default data
            gameData = new GameData();
            SaveGameDataToDisk();

            gameDataLoadStatus = LoadingStatus.Success;
            waitingGameDataAcknowledgement = true;
            GameController.Instance.loadingSceneController.ShowNotification("Notice", "Game save file load error. Initializing default values.");
        }
    }

    private bool TryLoadGameData(string path, out GameData data)
    {
        try
        {
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                data = JsonUtility.FromJson<GameData>(jsonData);
                return true;
            }
        }
        catch (Exception ex)
        {
            //TODO: Log, handle error. 
            Debug.LogError("Failed to read from file: " + path + ". Error: " + ex.Message);
        }
        data = null;
        return false;
    }

    /// <summary>
    /// Saves the current state of GameData class to disk.
    /// Remember to Update Game Data with Player Data for most up-to-date save.
    /// </summary>
    public void SaveGameDataToDisk()
    {
        string jsonData = JsonUtility.ToJson(gameData);

        //Attempt to write to primary file:
        bool primarySaveSuccess = TryWriteGameDataToDisk(saveGameDataPath, jsonData);
        if (!primarySaveSuccess)
        {
            //TODO: primary save file error, let it continue to the back up and then notify user, retry.
            Debug.LogWarning($"Primary Save File Write Error. In: {nameof(SaveGameDataToDisk)} : (!primarySaveSuccess)");
        }

        //Attempt to write to back up file
        bool backupSaveSuccess = TryWriteGameDataToDisk(saveGameDataPathBackup, jsonData);
        if (!backupSaveSuccess)
        {
            //TODO: Back up save file error, let the user know, reattempt, allow player to continue playing and trigger a save later in the game.
            Debug.LogWarning($"Primary Save File Write Error. In: {nameof(SaveGameDataToDisk)} : (!backupSaveSuccess)");
        }

        //TODO: notify player of save success, but only through a non-intrusive UI icon that doesn't require user input / interaction.
    }

    private bool TryWriteGameDataToDisk(string path, string data)
    {
        try
        {
            File.WriteAllText(path, data);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to write to file: " + path + ". Error: " + ex.Message);
            return false;
        }
    }

    #endregion
}
