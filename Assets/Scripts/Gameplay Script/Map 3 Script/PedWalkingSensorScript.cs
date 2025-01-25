using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ped.Waypoints
{
	public class PedWalkingSensorScript : MonoBehaviour
	{
		[SerializeField]
		PedWalkingScript MainPed;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnTriggerStay(Collider col)
		{
			if (col.CompareTag("Main Player"))
			{
				MainPed.StopMovement = true;
			}
		}

		private void OnTriggerExit(Collider col)
		{
			if (col.CompareTag("Main Player"))
			{
				MainPed.StopMovement = false;
			}
		}
	}
}

