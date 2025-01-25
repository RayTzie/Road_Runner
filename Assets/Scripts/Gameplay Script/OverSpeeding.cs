using UnityEngine;
using UnityEngine.UI;
using Deduction.Manager;

public class OverSpeeding : MonoBehaviour
{
	[SerializeField]
	DeductionManagerScript MainDeductionScript;
	public float speed; // The current speed of the car
    public float speedThreshold = 55f; // Speed threshold to trigger the canvas
    public GameObject speedCanvas; // Reference to the canvas you want to trigger

    void Update()
    {
        // Calculate the car's current speed (example assumes you're using a rigidbody for physics)
        Rigidbody rb = GetComponent<Rigidbody>();
		//speed = rb.velocity.magnitude * 1.6f; // Convert to km/h if velocity is in meters/second // 5f default Original value

		speed = rb.velocity.magnitude * 2.8f;

		// Check if the speed exceeds the threshold
		if (speed > speedThreshold)
        {
            speedCanvas.gameObject.SetActive(true); // Show the canvas
        }
        else
        {
            speedCanvas.gameObject.SetActive(false); // Hide the canvas
        }

		if (speed > MainDeductionScript.SpeedLimit)
		{
			MainDeductionScript.OverSpeedActivate = true;
		}
		else if (speed <= MainDeductionScript.SpeedLimit)
		{
			MainDeductionScript.OverSpeedActivate = false;
		}


	}

    public float GetSpeed(){
       return speed;
    }
}
