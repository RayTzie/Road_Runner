using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deduction.Manager;

namespace AICar
{
	public class CarAIBodyScript : MonoBehaviour
	{
		[SerializeField]
		DeductionManagerScript MainDeduction;
		[SerializeField]
		GameObject SensorObj;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.tag == "Player")
			{
				//Debug.Log("Hit Border");
				MainDeduction.OvertakingDirectionActivate();
			}

		}

		private void OnTriggerEnter(Collider col)
		{
			//Debug.Log("Hit Border");
			if (col.CompareTag("AI Point"))
			{
				SensorObj.gameObject.SetActive(false);
			}
		}
	}
}

