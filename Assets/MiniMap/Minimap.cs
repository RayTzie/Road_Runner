using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;                   // The player (car) object to follow
    public LineRenderer lineRenderer;          // The LineRenderer for the path
    public Transform[] waypoints;              // Waypoints for the line renderer
    public float forwardOffset = 5f;           // Distance ahead of the player to focus on
    public float size = 10f;                   // Size of the orthographic view for the minimap camera
    public Camera minimapCamera;
    public bool useOffset = false;            // Boolean to toggle offset mode

    void Start()
    {
        // Get the Camera component and ensure it's in orthographic mode
        minimapCamera = GetComponent<Camera>();
        if (minimapCamera != null)
        {
            minimapCamera.orthographic = true;
        }

        // Draw the initial route on the LineRenderer
        DrawRoute();
    }

    void LateUpdate()
    {
        // Apply the orthographic size in LateUpdate in case other scripts are altering it
        if (minimapCamera != null && minimapCamera.orthographic)
        {
            minimapCamera.orthographicSize = size;
        }

        if (useOffset)
        {
            // Apply the offset view
            ApplyOffsetView();
        }
        else
        {
            // Apply the default (no offset) view
            ApplyDefaultView();
        }
    }

    void ApplyDefaultView()
    {
        // Set the camera position to the player's position (centered)
        Vector3 defaultPosition = player.position;
        defaultPosition.y = transform.position.y;  // Maintain the same height for the minimap camera
        transform.position = defaultPosition;

        // Rotate the minimap camera to match the player's Y rotation
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    void ApplyOffsetView()
    {
        // Calculate the forward offset position relative to the player's forward direction
        Vector3 offsetPosition = player.position + player.forward * forwardOffset;
        offsetPosition.y = transform.position.y;  // Maintain the same height for the minimap camera

        // Update the minimap camera's position with the offset
        transform.position = offsetPosition;

        // Rotate the minimap camera to match the player's Y rotation
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    // Toggle the use of the offset
    public void ToggleOffset(bool value)
    {
        useOffset = value;
    }

    void DrawRoute()
    {
        // Check if lineRenderer and waypoints are valid
        if (lineRenderer != null && waypoints.Length > 0)
        {
            // Set the number of points in the LineRenderer
            lineRenderer.positionCount = waypoints.Length;

            // Set each position in the LineRenderer
            for (int i = 0; i < waypoints.Length; i++)
            {
                lineRenderer.SetPosition(i, waypoints[i].position);
            }
        }
    }
}
