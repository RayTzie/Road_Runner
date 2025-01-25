using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AICar
{
	public class CarAIScript : MonoBehaviour
	{
		[SerializeField]
		GameObject CarSensorObj;
		[SerializeField]
		CarAI MainCarAI;

		[Space]
		[Header("Settings")]
		[SerializeField]
		bool M1;
		[SerializeField]
		bool M2;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnTriggerEnter(Collider col)
		{
			//Debug.Log("Hit Border");
			if (col.CompareTag("Main Player"))
			{
				if (M1)
				{
					MainCarAI.move = true;
					CarSensorObj.gameObject.SetActive(true);

					this.gameObject.SetActive(false);
				}

				if (M2)
				{
					MainCarAI.move = false;
				}
			}

			if (M2)
			{
				if (col.CompareTag("AI Vehicle"))
				{
					MainCarAI.move = false;
				}
			}
				
		}

		private void OnTriggerExit(Collider col)
		{
			if (M2)
			{
				if (col.CompareTag("AI Vehicle"))
				{
					MainCarAI.move = true;
				}

				if (col.CompareTag("Main Player"))
				{
					MainCarAI.move = true;
				}
			}
		}
	}
}

