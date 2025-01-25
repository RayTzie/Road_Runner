using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobile.SnapDragon.Asyncsystem;

namespace Mobile.GPU.Asyncsystem
{
	public class AlmostEngineLimit : EngineScript
	{
		[HideInInspector]
		public SteeringWheel_Controller SteeringAngle;
		[HideInInspector]
		public Pedals_Controller TransmissionState, BreakValue, BreakInput, Speed;
		[HideInInspector]
		public OverSpeeding SpeedMeter;
		//public Speedometer Speed;

		[HideInInspector]
		public float m_horizontalInput;
		[HideInInspector]
		public float m_verticalInput;
		[HideInInspector]
		public float m_breakInputValue;
		[HideInInspector]
		public float m_steeringAngle; // Steering Angle Value 
		[HideInInspector]
		public float m_breakInput;
		[HideInInspector]
		public bool isbreaking = false;
		//public KeyCode m_breakInput = KeyCode.B;

		[HideInInspector]
		public WheelCollider frontDriverW, frontPassengerW;
		[HideInInspector]
		public WheelCollider rearDriverW, rearPassengerW;
		[HideInInspector]
		public Transform frontDriverT, frontPassengerT;
		[HideInInspector]
		public Transform rearDriverT, rearPassengerT;
		[HideInInspector]
		public Transform steeringWheel; // Steering Wheel
		[HideInInspector]
		public float maxSteerAngle;
		[HideInInspector]
		[SerializeField]
		float DefaultmaxSteerAngle;
		[HideInInspector]
		public float motorForce;
		[HideInInspector]
		[SerializeField]
		float DefaultMotorForce;
		[HideInInspector]
		public float breakForce;
		[HideInInspector]
		public float speed;
		[HideInInspector]
		public bool HandBrake;
		public void Started()
		{		

			SteeringAngle = GameObject.FindWithTag("Steering Wheel").GetComponent<SteeringWheel_Controller>();
			TransmissionState = GameObject.FindWithTag("Pedals").GetComponent<Pedals_Controller>();
			Speed = GameObject.FindWithTag("Pedals").GetComponent<Pedals_Controller>();
			SpeedMeter = GameObject.FindWithTag("Player").GetComponent<OverSpeeding>();
			BreakValue = GameObject.FindWithTag("Pedals").GetComponent<Pedals_Controller>();
			BreakInput = GameObject.FindWithTag("Pedals").GetComponent<Pedals_Controller>();

			maxSteerAngle = DefaultmaxSteerAngle;
		}

		public void GetInput()
		{
			m_horizontalInput = SteeringAngle.GetClampedValue();
			m_verticalInput = TransmissionState.GetAccelerator();
			//m_horizontalInput = Input.GetAxis("Horizontal");
			//m_verticalInput = Input.GetAxis("Vertical");

		}
		public void GetBreakInput()
		{
			m_breakInputValue = BreakInput.GetBreakValue();
		}

		public void Accelerate()
		{
			//frontDriverW.motorTorque = m_verticalInput * motorForce;
			//frontPassengerW.motorTorque = m_verticalInput * motorForce;

			// The motor force value is return by maxSpeed variable from Speedometer script.
			motorForce = DefaultMotorForce;
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
			rearDriverW.brakeTorque = m_breakInputValue;
			rearPassengerW.brakeTorque = m_breakInputValue;



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

		private void Updated()
		{
			if (HandBrake)
			{
				HandBrakeActivate();
			}
		}

		private void FixedUpdated()
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

		public float GetSpeedometer()
		{
			return speed;
		}
	}
}

