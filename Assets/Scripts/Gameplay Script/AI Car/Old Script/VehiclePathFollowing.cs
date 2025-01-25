using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehiclePathFollowing : MonoBehaviour
{
    public List<Transform> waypoints;             // List of waypoints along the road
    public float speed = 5f;                      // Base speed of the vehicle
    public float waypointTolerance = 0.5f;        // Distance to consider a waypoint reached
    public float yieldingDistance = 5f;           // Distance at which the vehicle will yield
    public LayerMask vehicleLayerMask;            // Layer mask to detect other vehicles
    public float overtakeOffset = 3f;             // Lateral offset for overtaking
    public GameObject player;                     // Reference to the player car
    public Canvas Mission_Failed;                  // Canvas to show on successful overtake

    private int currentWaypointIndex = 0;         // Index of the current waypoint
    private bool isOvertaking = false;            // Determines if the vehicle is overtaking
    private Vector3 overtakeTarget;               // Temporary overtake target position
    private static bool playerOvertakenCar1 = false;   // Tracks if the player has overtaken car 1
    private static bool playerOvertakenCar2 = false;   // Tracks if the player has overtaken car 2
    private static bool hasPlayerCompletedOvertake = false; // Flag for final debug message

    void Start()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned for the path!");
            return;
        }

        // Set the initial position to the first waypoint
        transform.position = waypoints[0].position;

        // Make sure the success canvas is hidden at the start
        if (Mission_Failed != null)
        {
            Mission_Failed.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check for other vehicles in front
        DetectVehiclesAhead();

        // Move towards the current or overtake target waypoint
        if (isOvertaking)
        {
            MoveTowards(overtakeTarget);
        }
        else if (currentWaypointIndex < waypoints.Count)
        {
            MoveTowards(waypoints[currentWaypointIndex].position);
        }
        else
        {
            Debug.Log("Vehicle has reached the final destination.");
        }

        // Check if the player has overtaken this specific AI car
        CheckIfPlayerHasOvertaken();

        // Check if the player has completed overtaking both AI cars
        if (!hasPlayerCompletedOvertake && playerOvertakenCar1 && playerOvertakenCar2)
        {
            hasPlayerCompletedOvertake = true;
            Debug.Log("Player has successfully overtaken both AI cars!");

            // Show the success canvas when both cars are overtaken
            if (Mission_Failed != null)
            {
                Mission_Failed.gameObject.SetActive(true);
            }
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        // Move the vehicle towards the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate the vehicle to face the target
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        // Check if the vehicle is close enough to the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < waypointTolerance)
        {
            if (!isOvertaking)
                currentWaypointIndex++;
            else
                isOvertaking = false;  // Stop overtaking after reaching the overtake target
        }
    }

    void DetectVehiclesAhead()
    {
        // Cast a ray forward to detect other vehicles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, yieldingDistance, vehicleLayerMask))
        {
            // If another vehicle is within yielding distance, start overtaking
            if (!isOvertaking)
            {
                isOvertaking = true;
                // Set an overtake target to the side (left or right)
                overtakeTarget = waypoints[currentWaypointIndex].position + transform.right * overtakeOffset;
            }
        }
    }

    void CheckIfPlayerHasOvertaken()
    {
        // Identify the specific AI car instance based on its unique name or tag
        if (transform.name == "AI Car 1" && player.transform.position.z > transform.position.z && !playerOvertakenCar1)
        {
            playerOvertakenCar1 = true;
            Debug.Log("Player has overtaken AI Car 1.");
             Mission_Failed.gameObject.SetActive(true);
        }
        else if (transform.name == "AI Car 2" && player.transform.position.z > transform.position.z && !playerOvertakenCar2)
        {
            playerOvertakenCar2 = true;
            Debug.Log("Player has overtaken AI Car 2.");
             Mission_Failed.gameObject.SetActive(true);
        }
    }
}
