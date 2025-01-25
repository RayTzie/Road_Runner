using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Deduction.Manager;

namespace TrafficLight
{
	public class TrafficLightScript : MonoBehaviour
	{
		[SerializeField]
		BoxCollider StartTrafficLightSensorCol;
		[SerializeField]
		BoxCollider FieldTrafficLightSensorCol;
		[SerializeField]
		GameObject[] TrafficLightColorObj;
		[SerializeField]
		GameObject NotificationObj;
		//[SerializeField]
		//DeductionManagerScript MainDeduction;
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
				NotificationObj.gameObject.SetActive(true);
				StartTrafficLightSensorCol.enabled = false;
				StartCoroutine("TrafficLightActivate");
			}
		}

		IEnumerator TrafficLightActivate()
		{
			yield return new WaitForSeconds(5f);
			foreach(GameObject TLCO in TrafficLightColorObj)
			{
				TLCO.gameObject.SetActive(false);
			}

			TrafficLightColorObj[1].gameObject.SetActive(true);

			yield return new WaitForSeconds(1f);
			foreach (GameObject TLCO in TrafficLightColorObj)
			{
				TLCO.gameObject.SetActive(false);
			}

			TrafficLightColorObj[2].gameObject.SetActive(true);
			FieldTrafficLightSensorCol.enabled = false;
		}
	}
}

