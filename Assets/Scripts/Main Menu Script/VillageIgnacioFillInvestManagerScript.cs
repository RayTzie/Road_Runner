using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JSON.System.Manager;
using UnityEngine.SceneManagement;
using Road.Manager;
using Deduction.Manager;

namespace Map.Manager
{
	public class VillageIgnacioFillInvestManagerScript : MonoBehaviour
	{
		[SerializeField]
		JSONVillageIgnacioFillInvestManagerScript JSONMainMap;
		[Header("Gameplay Settings")]
		public bool SpeedLimit;
		public bool Turns;
		public bool OverTaking;


		[Space]
		[Header("Game Value Settings")]
		public int SpeedLimitValue;
		[SerializeField]
		int SpeedLimitMaxValue;
		public int TurnsValue;
		[SerializeField]
		int TurnsMaxValue;
		public int OverTakingValue;
		[SerializeField]
		int OverTakingMaxValue;

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
		GameObject SpeedLimitObjMission;
		[SerializeField]
		GameObject SpeedLimitPathObj;
		[SerializeField]
		GameObject TurnsObjMission;
		[SerializeField]
		GameObject TurnsPathObj;
		[SerializeField]
		GameObject OverTakingObjMission;
		[SerializeField]
		GameObject OverTakingPathObj;
		[SerializeField]
		GameObject MissionObj;
		[SerializeField]
		RoadDirectionScript MainRoadDirection;
		[SerializeField]
		DeductionManagerScript MainDeduction;

		// Start is called before the first frame update
		void Start()
		{

		}

		public void InitialExecute()
		{
			if (MainMenuScene)
			{
				SpeedLimitValueText.text = SpeedLimitValue + "/" + SpeedLimitMaxValue;
				TurnsValueText.text = TurnsValue + "/" + TurnsMaxValue;
				OverTakingValueText.text = OverTakingValue + "/" + OverTakingMaxValue;
			}

			if (GameplayScene)
			{
				if (SpeedLimit)
				{
					MainDeduction.SpeedLimit= 20;
					SpeedLimitObjMission.gameObject.SetActive(true);
					SpeedLimitPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map1SettingsActivate();
				}

				if (Turns)
				{
					MainDeduction.SpeedLimit = 100;
					TurnsObjMission.gameObject.SetActive(true);
					TurnsPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map2SettingsActivate();
				}

				if(OverTaking)
				{
					MainDeduction.SpeedLimit = 100;
					OverTakingObjMission.gameObject.SetActive(true);
					OverTakingPathObj.gameObject.SetActive(true);
					MainRoadDirection.Map3SettingsActivate();
				}
			}
			
		}

		public void LoadMap(int MapID)
		{
			if (MapID == 0)
			{
				SpeedLimit = true;
				Turns = false;
				OverTaking = false;
			}

			if (MapID == 1)
			{
				SpeedLimit = false;
				Turns = true;
				OverTaking = false;
			}

			if (MapID == 2)
			{
				SpeedLimit = false;
				Turns = false;
				OverTaking = true;
			}

			JSONMainMap.TempSave();
			SceneManager.LoadScene(MapName);
		}

		public void DisplayGameplayMission()
		{
			if (MissionObj.activeSelf == false)
			{
				MissionObj.gameObject.SetActive(true);
			}

			else if (MissionObj.activeSelf == true)
			{
				MissionObj.gameObject.SetActive(false);
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

