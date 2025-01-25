using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road.Manager
{
	public class RoadDirectionScript : MonoBehaviour
	{
		[SerializeField]
		Minimap MainMiniMap;

		[Space]
		[Header("Map 1 Settings")]
		[SerializeField]
		Transform[] TM1;
		[SerializeField]
		MeshRenderer[] RM1;
		[SerializeField]
		BoxCollider[] BM1;
		[SerializeField]
		LineRenderer M1LineRenderer;

		[Space]
		[Header("Map 2 Settings")]
		[SerializeField]
		Transform[] TM2;
		[SerializeField]
		BoxCollider[] BM2;
		[SerializeField]
		MeshRenderer[] RM2;
		[SerializeField]
		LineRenderer M2LineRenderer;

		[Space]
		[Header("Map 3 Settings")]
		[SerializeField]
		Transform[] TM3;
		[SerializeField]
		MeshRenderer[] RM3;
		[SerializeField]
		BoxCollider[] BM3;
		[SerializeField]
		LineRenderer M3LineRenderer;
		// Start is called before the first frame update
		void Start()
		{

		}

		public void Map1SettingsActivate()
		{
			for (int i = 0; i < TM1.Length; i++)
			{
				MainMiniMap.waypoints.Add(TM1[i]);
			}

			for (int i = 0; i < RM1.Length; i++)
			{
				MainMiniMap.wayPointsMesh.Add(RM1[i]);
			}

			for (int i = 0; i < BM1.Length; i++)
			{
				MainMiniMap.wayPointsBCol.Add(BM1[i]);
			}

			MainMiniMap.lineRenderer = M1LineRenderer;
			MainMiniMap.InitialCheck();
			
		}

		public void Map2SettingsActivate()
		{
			for (int i = 0; i < TM2.Length; i++)
			{
				MainMiniMap.waypoints.Add(TM2[i]);
			}

			for (int i = 0; i < RM2.Length; i++)
			{
				MainMiniMap.wayPointsMesh.Add(RM2[i]);
			}

			for (int i = 0; i < BM2.Length; i++)
			{
				MainMiniMap.wayPointsBCol.Add(BM2[i]);
			}

			MainMiniMap.lineRenderer = M2LineRenderer;
			MainMiniMap.InitialCheck();
		}

		public void Map3SettingsActivate()
		{
			for (int i = 0; i < TM3.Length; i++)
			{
				MainMiniMap.waypoints.Add(TM3[i]);
			}

			for (int i = 0; i < RM3.Length; i++)
			{
				MainMiniMap.wayPointsMesh.Add(RM3[i]);
			}

			for (int i = 0; i < BM3.Length; i++)
			{
				MainMiniMap.wayPointsBCol.Add(BM3[i]);
			}

			MainMiniMap.lineRenderer = M3LineRenderer;
			MainMiniMap.InitialCheck();
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}

