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
	public class CentroPuebloManagerScript : MonoBehaviour
	{
		[SerializeField]
		JSONCentroPuebloManagerScript JSONMainMap;
		[Header("Gameplay Settings")]
		public bool OneWays;
		public bool Pedestrians;
		public bool NoLeftTurns;


		[Space]
		[Header("Game Value Settings")]
		public int OneWaysValue;
		[SerializeField]
		int OneWaysMaxValue;
		public int PedestriansValue;
		[SerializeField]
		int PedestriansMaxValue;
		public int NoLeftTurnsValue;
		[SerializeField]
		int NoLeftTurnsMaxValue;

		[Space]
		[Header("Text Settings")]
		[SerializeField]
		TextMeshProUGUI OneWaysValueText;
		[SerializeField]
		TextMeshProUGUI PedestriansValueText;
		[SerializeField]
		TextMeshProUGUI NoLeftTurnsValueText;

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
		GameObject OneWaysObjMission;
		[SerializeField]
		GameObject OneWaysPathObj;
		[SerializeField]
		GameObject PedestriansObjMission;
		[SerializeField]
		GameObject PedestriansPathObj;
		[SerializeField]
		GameObject LegalTurnsObjMission;
		[SerializeField]
		GameObject LegalTurnsPathObj;
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
				OneWaysValueText.text = OneWaysValue + "/" + OneWaysMaxValue;
				PedestriansValueText.text = PedestriansValue + "/" + PedestriansMaxValue;
				NoLeftTurnsValueText.text = NoLeftTurnsValue + "/" + NoLeftTurnsMaxValue;
			}

			if (GameplayScene)
			{
				if (OneWays)
				{
					PlayerVehicleObj.MovePosition(M1StartPoint.position);
					PlayerVehicleObj.MoveRotation(M1StartPoint.rotation);

					MainDeduction.SpeedLimit = 100;
					OneWaysObjMission.gameObject.SetActive(true);
					OneWaysPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map1SettingsActivate();
				}

				if (Pedestrians)
				{
					PlayerVehicleObj.MovePosition(M2StartPoint.position);
					PlayerVehicleObj.MoveRotation(M2StartPoint.rotation);

					MainDeduction.SpeedLimit = 100;
					PedestriansObjMission.gameObject.SetActive(true);
					PedestriansPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map2SettingsActivate();
				}

				if (NoLeftTurns)
				{
					PlayerVehicleObj.MovePosition(M3StartPoint.position);
					PlayerVehicleObj.MoveRotation(M3StartPoint.rotation);

					MainDeduction.SpeedLimit = 100;
					LegalTurnsObjMission.gameObject.SetActive(true);
					LegalTurnsPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map3SettingsActivate();
				}
			}
			
		}

		public void LoadMap(int MapID)
		{
			if (MapID == 0)
			{
				OneWays = true;
				Pedestrians = false;
				NoLeftTurns = false;
			}

			if (MapID == 1)
			{
				OneWays = false;
				Pedestrians = true;
				NoLeftTurns = false;
			}

			if (MapID == 2)
			{
				OneWays = false;
				Pedestrians = false;
				NoLeftTurns = true;
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

