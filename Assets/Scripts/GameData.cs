using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores active Game Data for Json Interoperability.
/// </summary>
public class GameData 
{
    //This only needs to be updated when I am prepping save and load from file operations.

    //TODO: Ensure this is synced with active play classes: Player, Ship, Pilot
    //TODO: Method: UpdateSaveGameDataWithPlayerData() & UpdatePlayerDataWithSavedGameData() in DataController
    //need to be updated with any property / variable changes




    public int selectedPilotIndex;


    
    /// <summary>
    /// Default Values Constructor.
    /// </summary>
    public GameData()
    {

        selectedPilotIndex = 0;
    }

}
