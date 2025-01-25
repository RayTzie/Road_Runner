using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JSON.System.Manager;
using UnityEngine.SceneManagement;
using Deduction.Manager;
using Road.Manager;

namespace Map.Manager
{
	public class MayorJaldonManagerScript : MonoBehaviour
	{
		[SerializeField]
		JSONMayorJaldonStreetManagerScript JSONMainMap;
		[Header("Gameplay Settings")]
		public bool UTurns;
		public bool VehicleYielding;
		public bool Skyway;


		[Space]
		[Header("Game Value Settings")]
		public int UTurnsValue;
		[SerializeField]
		int UTurnsMaxValue;
		public int VehicleYieldingValue;
		[SerializeField]
		int VehicleYieldingMaxValue;
		public int SkywayValue;
		[SerializeField]
		int SkywayMaxValue;

		[Space]
		[Header("Text Settings")]
		[SerializeField]
		TextMeshProUGUI SpeedLimitValueText;
		[SerializeField]
		TextMeshProUGUI TurnsValueText;
		[SerializeField]
		TextMeshProUGUI OverTakingValueText;

		[Space]
		[Header("Scene Settings")]
		[SerializeField]
		string MapName;

		[Space]
		[Header("Scene Settings")]
		[SerializeField]
		bool MainMenuScene;
		[SerializeField]
		bool GameplayScene;

		[Space]
		[Header("Gameplay Settings")]
		[SerializeField]
		GameObject UTurnsObjMission;
		[SerializeField]
		GameObject UTurnsPathObj;
		[SerializeField]
		GameObject VehicleYieldingObjMission;
		[SerializeField]
		GameObject VehicleYieldingPathObj;
		[SerializeField]
		GameObject SkywayObjMission;
		[SerializeField]
		GameObject SkywayPathObj;
		[SerializeField]
		GameObject MissionObj;
		[SerializeField]
		RoadDirectionScript MainRoadDirection;
		[SerializeField]
		DeductionManagerScript MainDeduction;

		[Space]
		[Header("Game Start Position")]
		[SerializeField]
		Transform M1StartPoint;
		[SerializeField]
		Transform M2StartPoint;
		[SerializeField]
		Transform M3StartPoint;
		[SerializeField]
		Rigidbody PlayerVehicleObj;
		// Start is called before the first frame update
		void Start()
		{

		}

		public void InitialExecute()
		{
			if (MainMenuScene)
			{
				SpeedLimitValueText.text = UTurnsValue + "/" + UTurnsMaxValue;
				TurnsValueText.text = VehicleYieldingValue + "/" + VehicleYieldingMaxValue;
				OverTakingValueText.text = SkywayValue + "/" + SkywayMaxValue;
			}
			
			if (GameplayScene)
			{
				if(UTurns)
				{
					PlayerVehicleObj.MovePosition(M1StartPoint.position);
					PlayerVehicleObj.MoveRotation(M1StartPoint.rotation);

					MainDeduction.SpeedLimit = 100;
					UTurnsObjMission.gameObject.SetActive(true);
					UTurnsPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map1SettingsActivate();
				}

				if (VehicleYielding)
				{
					PlayerVehicleObj.MovePosition(M2StartPoint.position);
					PlayerVehicleObj.MoveRotation(M2StartPoint.rotation);

					MainDeduction.SpeedLimit = 100;
					VehicleYieldingObjMission.gameObject.SetActive(true);
					VehicleYieldingPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map2SettingsActivate();
				}

				if (Skyway)
				{
					PlayerVehicleObj.MovePosition(M3StartPoint.position);
					PlayerVehicleObj.MoveRotation(M3StartPoint.rotation);

					MainDeduction.SpeedLimit = 100;
					SkywayObjMission.gameObject.SetActive(true);
					SkywayPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map3SettingsActivate();
				}
			}
		}

		public void LoadMap(int MapID)
		{
			if (MapID == 0)
			{
				UTurns = true;
				VehicleYielding = false;
				Skyway = false;
			}

			if (MapID == 1)
			{
				UTurns = false;
				VehicleYielding = true;
				Skyway = false;
			}

			if (MapID == 2)
			{
				UTurns = false;
				VehicleYielding = false;
				Skyway = true;
			}

			JSONMainMap.TempSave();
			SceneManager.LoadScene(MapName);
		}

		public void DisplayGameplayMission()
		{
			if (!MissionObj.activeSelf)
			{
				MissionObj.gameObject.SetActive(true);
				Debug.Log("Show");
			}

			else if (MissionObj.activeSelf)
			{
				MissionObj.gameObject.SetActive(false);
				Debug.Log("Hide");
			}


		}

		public void TempSave()
		{
			JSONMainMap.TempSave();
		}
		// Update is called once per frame
		void Update()
		{
			
		}
	}
}

