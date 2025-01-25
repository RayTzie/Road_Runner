using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobile.AsyncAndroid;

public class SimpleCarController : MonoBehaviour {

 	public SteeringWheel_Controller SteeringAngle;
 	public Pedals_Controller TransmissionState, BreakValue,BreakInput,Speed;
 	public OverSpeeding SpeedMeter;
 	//public Speedometer Speed;
 

 	public float m_horizontalInput;
	public float m_verticalInput;
	public float m_breakInputValue;
	public float m_steeringAngle; // Steering Angle Value 
	public float m_breakInput;
	public bool isbreaking = false;
	//public KeyCode m_breakInput = KeyCode.B;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	
	public Transform steeringWheel; // Steering Wheel

	public float maxSteerAngle;
	[SerializeField]
	float DefaultmaxSteerAngle;
	public float motorForce;
	[SerializeField]
	float DefaultMotorForce;
	public float breakForce;
	public float speed;

	public bool HandBrake;
	AndroidSnapSystem AsyncSnapDragon;
	public void Start()
	{

		AsyncSnapDragon = GameObject.FindObjectOfType<AndroidSnapSystem>();

		if (AsyncSnapDragon == null)
		{
			Destroy(this.gameObject);
		}

		SteeringAngle = GameObject.FindWithTag ("Steering Wheel").GetComponent<SteeringWheel_Controller>();
		  TransmissionState = GameObject.FindWithTag ("Pedals").GetComponent<Pedals_Controller>();
		  Speed = GameObject.FindWithTag ("Pedals").GetComponent<Pedals_Controller>();
		  SpeedMeter = GameObject.FindWithTag ("Player").GetComponent<OverSpeeding>();
		  BreakValue = GameObject.FindWithTag ("Pedals").GetComponent<Pedals_Controller>();
		  BreakInput = GameObject.FindWithTag ("Pedals").GetComponent<Pedals_Controller>();

		maxSteerAngle = DefaultmaxSteerAngle;
	}

	public void GetInput()
	{
		m_horizontalInput = SteeringAngle.GetClampedValue();
		m_verticalInput = TransmissionState.GetAccelerator();
		//m_horizontalInput = Input.GetAxis("Horizontal");
		//m_verticalInput = Input.GetAxis("Vertical");
		
	}
	public void GetBreakInput(){
		m_breakInputValue = BreakInput.GetBreakValue();
	}

		public void Accelerate()
	{
		//frontDriverW.motorTorque = m_verticalInput * motorForce;
		//frontPassengerW.motorTorque = m_verticalInput * motorForce;
		
		// The motor force value is return by maxSpeed variable from Speedometer script.
       motorForce  = DefaultMotorForce;
       //Debug.Log(motorForce);
       
    	// The speed value is used to trigger/update the speed on speedometer script.
        //speed =  Mathf.Clamp (speed += motorForce * Time.deltaTime  , 0, motorForce);
        
		rearDriverW.motorTorque = m_verticalInput * motorForce;
		rearPassengerW.motorTorque = m_verticalInput * motorForce;

		speed = SpeedMeter.GetSpeed();


	}


		public void Brake()
    {
    	 m_breakInputValue = BreakInput.GetBreakValue();
    	 
    	 frontDriverW.brakeTorque = m_breakInputValue;
		 frontPassengerW.brakeTorque = m_breakInputValue;
    	 rearDriverW.brakeTorque =m_breakInputValue;
		 rearPassengerW.brakeTorque =m_breakInputValue;

    	

    }

	public void HandBrakeActivate()
	{
		//m_breakInputValue = BreakInput.GetBreakValue();

		frontDriverW.brakeTorque = 500;
		frontPassengerW.brakeTorque = 500;
		rearDriverW.brakeTorque = 500;
		rearPassengerW.brakeTorque = 500;
	}


	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;

	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);

		
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void Update()
	{
		if (HandBrake)
		{
			HandBrakeActivate();
		}
	}

	private void FixedUpdate()
	{
		if (!HandBrake)
		{
			GetInput();
			Steer();
			Accelerate();

			Brake();
			GetBreakInput();
			UpdateWheelPoses();
		}
		
		//m_steeringAngle = SteeringAngle.wheelAngle;
		
	}

	public float GetSpeedometer(){
		return speed;
	}





}
