using UnityEngine;

public class Level1_1 : MonoBehaviour, ILevelSpecific
{
    public void CustomStart()
    {
        Debug.Log($"Running: {nameof(Level1_1)} >> {nameof(CustomStart)}");
    }
}