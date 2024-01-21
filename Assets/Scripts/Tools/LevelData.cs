using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level Data", order = 2)]
public class LevelData : ScriptableObject
{
    [Header("Level Reference - For Scene Management")]
    [Tooltip("Needs to match Scene Data Settings")]
    public int sceneIndex;
    [Tooltip("Unique for levels, doesn't include Scene Data indexes")]
    public int levelIndex;
    [Tooltip("Needs to match Scene Data Settings")]
    public string levelName;

    [Header("Level Attributes")]
    public string levelDescription;
    public float levelDifficultyMultiplier;

    [Header("Boundary Constraints")]
    public float maxRadius;
}
