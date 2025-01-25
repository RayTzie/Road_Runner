using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JSON.Database.Manager;
using Map.Manager;

namespace JSON.System.Manager
{
	public class JSONVillageIgnacioFillInvestManagerScript : MonoBehaviour
	{
		[SerializeField]
		VillageIgnacioFillInvestManagerScript MainMapScript;
		[Header("Save Settings")]
		[SerializeField]
		string FolderPath;
		[SerializeField]
		string FileName;
		string DIRPath;
		bool PCSave;
		bool AndroidSave;

		private void Awake()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				PCSave = false;
				AndroidSave = true;
			}

			if (Application.platform == RuntimePlatform.WindowsEditor)
			{
				PCSave = true;
				AndroidSave = false;
			}
		}
		// Start is called before the first frame update
		void Start()
		{
			JSONLoad();
		}

		public void TempSave()
		{
			JSONSave();
		}
		JSONVillageIgnacioFillInvestDatabaseScript CreateData()
		{
			JSONVillageIgnacioFillInvestDatabaseScript save = new JSONVillageIgnacioFillInvestDatabaseScript();

			save._DATASpeedLimit = MainMapScript.SpeedLimit;
			save._DATATurns = MainMapScript.Turns;
			save._DATAOvertaking = MainMapScript.OverTaking;

			save._DATASpeedLimitValue = MainMapScript.SpeedLimitValue;
			save._DATATurnsValue = MainMapScript.TurnsValue;
			save._DATAOvertakingValue = MainMapScript.OverTakingValue;

			return save;
		}

		void JSONSave()
		{
			JSONVillageIgnacioFillInvestDatabaseScript save = CreateData();

			if (PCSave)
			{
				DIRPath = Path.Combine(FolderPath);
			}

			else if (AndroidSave)
			{
				DIRPath = Path.Combine(Application.persistentDataPath);
			}

			if (!Directory.Exists(DIRPath))
			{
				Directory.CreateDirectory(DIRPath);
				
			}

			string JSONString = JsonUtility.ToJson(save);
			StreamWriter sw = new StreamWriter(DIRPath + FileName);
			sw.Write(JSONString);
			sw.Close();
		}

		void JSONLoad()
		{
			if (PCSave)
			{
				DIRPath = Path.Combine(FolderPath);
			}

			else if (AndroidSave)
			{
				DIRPath = Path.Combine(Application.persistentDataPath);
			}

			if (File.Exists(DIRPath + FileName))
			{
				StreamReader sr = new StreamReader(DIRPath + FileName);
				string JsonString = sr.ReadToEnd();

				sr.Close();

				JSONVillageIgnacioFillInvestDatabaseScript load = JsonUtility.FromJson<JSONVillageIgnacioFillInvestDatabaseScript>(JsonString);

				MainMapScript.SpeedLimit = load._DATASpeedLimit;
				MainMapScript.Turns = load._DATATurns;
				MainMapScript.OverTaking = load._DATAOvertaking;

				MainMapScript.SpeedLimitValue = load._DATASpeedLimitValue;
				MainMapScript.TurnsValue = load._DATATurnsValue;
				MainMapScript.OverTakingValue = load._DATAOvertakingValue;
				MainMapScript.InitialExecute();

			}

			else
			{
				MainMapScript.SpeedLimit = false;
				MainMapScript.Turns = false;
				MainMapScript.OverTaking = false;

				MainMapScript.SpeedLimitValue = 0;
				MainMapScript.TurnsValue = 0;
				MainMapScript.OverTakingValue = 0;
				MainMapScript.InitialExecute();
				JSONSave();
			}
		}

		// Update is called once per frame
		//void Update()
		//{

		//}
	}
}

