using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Road.Sensor
{
	public class RoadSensorScript : MonoBehaviour
	{
		[SerializeField]
		Minimap MainMiniMap;
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
			if (col.CompareTag("Main Player"))
			{
				//Debug.Log("Sensor Hit");
				this.gameObject.SetActive(false);
				MainMiniMap.waypoints.Remove(MainMiniMap.waypoints[0]);
				MainMiniMap.wayPointsMesh.Remove(MainMiniMap.wayPointsMesh[0]);
				MainMiniMap.wayPointsBCol.Remove(MainMiniMap.wayPointsBCol[0]);
				MainMiniMap.LineRendererManualUpdate();
			}
		}
	}

}
