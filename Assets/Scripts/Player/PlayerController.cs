using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player is a Singleton Pattern Object
    public static PlayerController Instance { get; private set; }

   [Header("Subordinate Player Control Scripts")]
    public ShipController shipController;
    public PilotController pilotController;
    public FuelController fuelController;
    public PlayerMovementController playerMovementController;

    [Header ("Outside references")]
    [SerializeField] private Transform shipTransform;
    [HideInInspector] public LevelData currentLevelData;

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

        if (shipTransform == null)
        {
            Debug.LogError("Ship Transform not assigned in PlayerController");
        }
    }

    /// <summary>
    /// Use to get the reference to the Ship Transform when player object isn't in scene hierarch.
    /// </summary>
    /// <returns>The transform of the GameObjec Ship, which contains all of the 3D models.</returns>
    public Transform GetShipTransform()
    {
        if (shipTransform == null)
        {
            Debug.LogError("Ship Transform is not available in PlayerController");
        }
        return shipTransform;
    }

    public void SetLevelData(LevelData data)
    {
        currentLevelData = data;
    }

    public float GetTDriveRadiusExpander()
    {
        //20 January, this is used in playermovement to get the maxradius to fly away from TDrive, do anything extra here 
        //once I get "CurrentTDriveState()" from Ship Controller.

        //TODO: This will be an enum
        float tDriveState = PlayerController.Instance.shipController.CurrentTDriveState();

        //TODO: Do whatever check / switch / math needed to derive how much I want to add to the radius based on the tdrive state.
        float tDriveRadiusExpander = tDriveState + 0;

        return tDriveRadiusExpander;
    }
}
