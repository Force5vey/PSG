using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player is a Singleton Pattern Object
    public static PlayerController Instance { get; private set; }

    //Subordinate script components, assign in Unity inspector.
    public ShipController shipController;
    public PilotController pilotController;
    public FuelController fuelController;
    public PlayerMovementController playerMovementController;

    //Game objects / components for outside use
    [SerializeField] private Transform shipTransform;

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

}
