using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deduction.Manager;

namespace Block.Manager
{
	public class PhysicalBlockPrefabScript : MonoBehaviour
	{
		//[SerializeField]
		DeductionManagerScript MainDeduction;
		// Start is called before the first frame update
		void Start()
		{
			MainDeduction = GameObject.FindObjectOfType<DeductionManagerScript>();
		}

		// Update is called once per frame
		void Update()
		{

		}

		//private void OnTriggerEnter(Collider col)
		//{
		//	//Debug.Log("Hit Border");
		//	if (col.CompareTag("Main Player"))
		//	{
		//		MainDeduction.WrongWayDirectionActivate();
		//	}
		//}

		private void OnCollisionEnter(Collision col)
		{
			if (col.gameObject.tag == "Player")
			{
				//Debug.Log("Hit Border");
				MainDeduction.WrongWayDirectionActivate();
			}
		}
	}

}
