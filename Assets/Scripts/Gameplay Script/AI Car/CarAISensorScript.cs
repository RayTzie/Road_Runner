using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deduction.Manager;


namespace AICar
{
	public class CarAISensorScript : MonoBehaviour
	{
		[SerializeField]
		DeductionManagerScript MainDeduction;
		[SerializeField]
		GameObject NotifMessageObj;
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
				NotifMessageObj.gameObject.SetActive(true);
				MainDeduction.OvertakingDirectionActivate();
			}
		}
	}
}

