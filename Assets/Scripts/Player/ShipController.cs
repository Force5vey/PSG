using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float shipVariable = 2f;
    [SerializeField] PlayerMovementController playerMovementController;

    //Debug stuff
    [SerializeField] GameObject boundaryIndicator;

    private void OnEnable()
    {
        //playerMovementController.OnBoundaryCrossed += HandleBoundaryCrossed;
    }

    private void OnDisable()
    {
        //playerMovementController.OnBoundaryCrossed -= HandleBoundaryCrossed;
    }



    private void HandleBoundaryCrossed(bool isOutsideBoundary)
    {
    if(isOutsideBoundary)
        {
            // Deplete air supply

            //For current debugging
            boundaryIndicator.SetActive(true);

        }
    else
        {
            // stop air depletion and start refill
            //TODO: decision if the depletion just stops but need to return to T-Drive to replenish.

            //debugging use
            boundaryIndicator.SetActive(false);
        }
    }

    //TODO: CurrentTDriveState needs to be an enum that aligns with repair stages.
    public float CurrentTDriveState()
    {
        return 0f;
    }
}
