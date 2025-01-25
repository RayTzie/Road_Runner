using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Deduction.Manager;

namespace Ped.Waypoints
{
	public class PedWalkingScript : MonoBehaviour
	{
		[SerializeField]
		List<Transform> Waypoints = new List<Transform>();
		int CurrentWayPoint;
		public bool StopMovement;
		[SerializeField]
		float moveSpeed;
		int MaxWayPoints;
		[SerializeField]
		Animator Anim;
		[SerializeField]
		DeductionManagerScript MainDeduction;
		// Start is called before the first frame update
		void Start()
		{
			moveSpeed = Random.Range(.3f, .9f);
			for (int i = 0; i < Waypoints.Count; i++)
			{
				MaxWayPoints = i;
			}
		}

		void FindPoint()
		{
			if (Vector3.Distance(transform.position, Waypoints[CurrentWayPoint].position) < 1)
			{
				moveSpeed = Random.Range(.3f, 1.3f);
				if (CurrentWayPoint < MaxWayPoints)
				{
					CurrentWayPoint += 1;
				}
				else if (CurrentWayPoint >= MaxWayPoints)
				{
					CurrentWayPoint = 0;
				}
			}
		}

		// Update is called once per frame
		void Update()
		{
			FindPoint();

			if (!StopMovement)
			{
				Anim.SetBool("Walking", true);
				transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
				transform.LookAt(Waypoints[CurrentWayPoint]);
			}

			if (StopMovement)
			{
				Anim.SetBool("Walking", false);
				transform.Translate(Vector3.forward * 0 * Time.deltaTime);
			}
		}

		private void OnTriggerEnter(Collider col)
		{
			if (col.CompareTag("Main Player"))
			{
				this.gameObject.SetActive(false);
				MainDeduction.PedestrianHitActivate();
			}
		}
	}
}

