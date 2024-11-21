using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{
    [Header("Car Wheels (Wheel Collider)")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    [Header("Car Wheels (Transform)")]
    public Transform wheelFL;
    public Transform wheelFR;
    public Transform wheelBL;
    public Transform wheelBR;

    [Header("Car Front (Transform)")]
    public Transform carFront;

    [Header("General Parameters")]
    public List<string> NavMeshLayers;
    public int MaxSteeringAngle = 45;
    public int MaxRPM = 150;

    [Header("Debug")]
    public bool ShowGizmos;
    public bool Debugger;

    [Header("Destination Parameters")]
    public bool Patrol = true;
    public Transform CustomDestination;
   
    [HideInInspector] public bool move;

    private Vector3 PostionToFollow = Vector3.zero;
    public int currentWayPoint;
    private float AIFOV = 60;
    private bool allowMovement;
    private int NavMeshLayerBite;
    public List<Transform> manualWaypoints = new List<Transform>(); // Manually assign waypoints here
    private float LocalMaxSpeed;
    private int Fails;
    private float MovementTorque = 1;

    [Header("Raycast Obstacle Detection")]
    public float detectionDistance = 10f;  // Distance to check for obstacles
    public LayerMask obstacleLayer;  // Layer mask to specify which objects are considered obstacles

    void Awake()
    {
        currentWayPoint = 0;
        allowMovement = true;
        move = true;
    }

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        CalculateNavMashLayerBite();
    }

    void FixedUpdate()
    {
        UpdateWheels();
        ApplySteering();
        PathProgress();
        DetectObstacles(); // Add obstacle detection
    }

    private void CalculateNavMashLayerBite()
    {
        if (NavMeshLayers == null || NavMeshLayers[0] == "Street")
            NavMeshLayerBite = NavMesh.AllAreas;
        else if (NavMeshLayers.Count == 1)
            NavMeshLayerBite += 1 << NavMesh.GetAreaFromName(NavMeshLayers[0]);
        else
        {
            foreach (string Layer in NavMeshLayers)
            {
                int I = 1 << NavMesh.GetAreaFromName(Layer);
                NavMeshLayerBite += I;
            }
        }
    }

    private void PathProgress()
    {
        wayPointManager();
        Movement();
        ListOptimizer();

        void wayPointManager()
        {
            if (currentWayPoint >= manualWaypoints.Count)
            {
                if (Patrol)
                {
                    currentWayPoint = 0; // Loop back to the first waypoint
                }
                else
                {
                    allowMovement = false; // Stop moving if not patrolling
                }
            }
            else
            {
                PostionToFollow = manualWaypoints[currentWayPoint].position;
                allowMovement = true;
                if (Vector3.Distance(carFront.position, PostionToFollow) < 2)
                {
                    currentWayPoint++;
                }
            }
        }

        void ListOptimizer()
        {
            if (currentWayPoint > 1 && manualWaypoints.Count > 30)
            {
                manualWaypoints.RemoveAt(0);
                currentWayPoint--;
            }
        }
    }

    private bool CheckForAngle(Vector3 pos, Vector3 source, Vector3 direction)
    {
        Vector3 distance = (pos - source).normalized;
        float CosAngle = Vector3.Dot(distance, direction);
        float Angle = Mathf.Acos(CosAngle) * Mathf.Rad2Deg;

        return Angle < AIFOV;
    }

    private void ApplyBrakes()
    {
        frontLeft.brakeTorque = 5000;
        frontRight.brakeTorque = 5000;
        backLeft.brakeTorque = 5000;
        backRight.brakeTorque = 5000;
    }

    private void UpdateWheels()
    {
        ApplyRotationAndPostion(frontLeft, wheelFL);
        ApplyRotationAndPostion(frontRight, wheelFR);
        ApplyRotationAndPostion(backLeft, wheelBL);
        ApplyRotationAndPostion(backRight, wheelBR);
    }

    private void ApplyRotationAndPostion(WheelCollider targetWheel, Transform wheel)
    {
        targetWheel.ConfigureVehicleSubsteps(5, 12, 15);

        Vector3 pos;
        Quaternion rot;
        targetWheel.GetWorldPose(out pos, out rot);
        wheel.position = pos;
        wheel.rotation = rot;
    }

    void ApplySteering()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(PostionToFollow);
        float SteeringAngle = (relativeVector.x / relativeVector.magnitude) * MaxSteeringAngle;
        if (SteeringAngle > 15) LocalMaxSpeed = 100;
        else LocalMaxSpeed = MaxRPM;

        frontLeft.steerAngle = SteeringAngle;
        frontRight.steerAngle = SteeringAngle;
    }

    void Movement()
    {
        if (move == true && allowMovement == true)
            allowMovement = true;
        else
            allowMovement = false;

        if (allowMovement == true)
        {
            frontLeft.brakeTorque = 0;
            frontRight.brakeTorque = 0;
            backLeft.brakeTorque = 0;
            backRight.brakeTorque = 0;

            int SpeedOfWheels = (int)((frontLeft.rpm + frontRight.rpm + backLeft.rpm + backRight.rpm) / 4);

            if (SpeedOfWheels < LocalMaxSpeed)
            {
                backRight.motorTorque = 400 * MovementTorque;
                backLeft.motorTorque = 400 * MovementTorque;
                frontRight.motorTorque = 400 * MovementTorque;
                frontLeft.motorTorque = 400 * MovementTorque;
            }
            else if (SpeedOfWheels < LocalMaxSpeed + (LocalMaxSpeed * 1 / 4))
            {
                backRight.motorTorque = 0;
                backLeft.motorTorque = 0;
                frontRight.motorTorque = 0;
                frontLeft.motorTorque = 0;
            }
            else
                ApplyBrakes();
        }
        else
            ApplyBrakes();
    }

    void debug(string text, bool IsCritical)
    {
        if (Debugger)
        {
            if (IsCritical)
                Debug.LogError(text);
            else
                Debug.Log(text);
        }
    }

    private void DetectObstacles()
{
    RaycastHit hit;
    Vector3 rayOrigin = carFront.position;
    Vector3 rayDirection = carFront.forward;

   
    
    if (Physics.Raycast(rayOrigin, rayDirection, out hit, detectionDistance, obstacleLayer))
    {
        Debug.Log("Raycast hit: " + hit.collider.name);
       

        ApplyBrakes();
    }
    else
    {
        Debug.Log("No obstacle detected in front.");
    }

    // Cast rays angled 30 degrees to the left and right
    Vector3 leftRayDirection = Quaternion.Euler(0, -20, 0) * rayDirection;
    Vector3 rightRayDirection = Quaternion.Euler(0, 20, 0) * rayDirection;

    // Left angled raycast
    if (Physics.Raycast(rayOrigin, leftRayDirection, out hit, detectionDistance, obstacleLayer))
    {
        if (Debugger)
            Debug.Log("Obstacle detected to the left: " + hit.collider.name);

        ApplyBrakes();  // Apply brakes if an obstacle is detected to the left
    }

    // Right angled raycast
    if (Physics.Raycast(rayOrigin, rightRayDirection, out hit, detectionDistance, obstacleLayer))
    {
        if (Debugger)
            Debug.Log("Obstacle detected to the right: " + hit.collider.name);

        ApplyBrakes();  // Apply brakes if an obstacle is detected to the right
    }
}

private void OnDrawGizmos()
{
    if (ShowGizmos == true)
    {
        for (int i = 0; i < manualWaypoints.Count; i++)
        {
            if (i == currentWayPoint)
                Gizmos.color = Color.blue;
            else
            {
                if (i > currentWayPoint)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.green;
            }
            Gizmos.DrawWireSphere(manualWaypoints[i].position, 2f);
        }

        // Draw Raycast detection gizmos
        Gizmos.color = Color.yellow;
        Vector3 rayOrigin = carFront.position;
        Vector3 rayDirection = carFront.forward;
        
        Gizmos.DrawRay(rayOrigin, rayDirection * detectionDistance); // Forward ray
        Gizmos.DrawRay(rayOrigin, Quaternion.Euler(0, -20, 0) * rayDirection * detectionDistance); // Left angled ray
        Gizmos.DrawRay(rayOrigin, Quaternion.Euler(0, 20, 0) * rayDirection * detectionDistance);  // Right angled ray
    }
}

}
