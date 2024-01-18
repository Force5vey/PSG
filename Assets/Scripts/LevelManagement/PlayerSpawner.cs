using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [HideInInspector] public Vector3 spawnPosition;

    [Header("Gizmo")]
    public Color gizmoColor = Color.blue;
    public float gizmoSize = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoSize);
    }

    private void Awake()
    {
        spawnPosition = transform.position;
    }
}
