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
}
