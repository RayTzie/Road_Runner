using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JSON.Database.Manager;
using Map.Manager;

namespace JSON.System.Manager
{
	public class JSONMayorJaldonStreetManagerScript : MonoBehaviour
	{
		[SerializeField]
		MayorJaldonManagerScript MainMapScript;
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
		JSONMayorJaldonStreetDatabaseScript CreateData()
		{
			JSONMayorJaldonStreetDatabaseScript save = new JSONMayorJaldonStreetDatabaseScript();

			save._DATAUTurns = MainMapScript.UTurns;
			save._DATAVehicleYielding = MainMapScript.VehicleYielding;
			save._DATASkyway = MainMapScript.Skyway;

			save._DATAUTurnsValue = MainMapScript.UTurnsValue;
			save._DATAVehicleYieldingValue = MainMapScript.VehicleYieldingValue;
			save._DATASkywayValue = MainMapScript.SkywayValue;

			return save;
		}

		void JSONSave()
		{
			JSONMayorJaldonStreetDatabaseScript save = CreateData();

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

				JSONMayorJaldonStreetDatabaseScript load = JsonUtility.FromJson<JSONMayorJaldonStreetDatabaseScript>(JsonString);

				MainMapScript.UTurns = load._DATAUTurns;
				MainMapScript.VehicleYielding = load._DATAVehicleYielding;
				MainMapScript.Skyway = load._DATASkyway;

				MainMapScript.UTurnsValue = load._DATAUTurnsValue;
				MainMapScript.VehicleYieldingValue = load._DATAVehicleYieldingValue;
				MainMapScript.SkywayValue = load._DATASkywayValue;
				MainMapScript.InitialExecute();

			}

			else
			{
				MainMapScript.UTurns = false;
				MainMapScript.VehicleYielding = false;
				MainMapScript.Skyway = false;

				MainMapScript.UTurnsValue = 0;
				MainMapScript.VehicleYieldingValue = 0;
				MainMapScript.SkywayValue = 0;
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

