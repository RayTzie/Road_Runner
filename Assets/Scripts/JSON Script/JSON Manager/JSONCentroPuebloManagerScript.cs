using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JSON.Database.Manager;
using Map.Manager;

namespace JSON.System.Manager
{
	public class JSONCentroPuebloManagerScript : MonoBehaviour
	{
		[SerializeField]
		CentroPuebloManagerScript MainMapScript;
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
		JSONCentroPuebloDatabaseScript CreateData()
		{
			JSONCentroPuebloDatabaseScript save = new JSONCentroPuebloDatabaseScript();

			save._DATAOneWays = MainMapScript.OneWays;
			save._DATAPedestrians = MainMapScript.Pedestrians;
			save._DATANoLeftTurns = MainMapScript.NoLeftTurns;

			save._DATAOneWaysValue = MainMapScript.OneWaysValue;
			save._DATAPedestriansValue = MainMapScript.PedestriansValue;
			save._DATANoLeftTurnsValue = MainMapScript.NoLeftTurnsValue;

			return save;
		}

		void JSONSave()
		{
			JSONCentroPuebloDatabaseScript save = CreateData();

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

				JSONCentroPuebloDatabaseScript load = JsonUtility.FromJson<JSONCentroPuebloDatabaseScript>(JsonString);

				MainMapScript.OneWays = load._DATAOneWays;
				MainMapScript.Pedestrians = load._DATAPedestrians;
				MainMapScript.NoLeftTurns = load._DATANoLeftTurns;

				MainMapScript.OneWaysValue = load._DATAOneWaysValue;
				MainMapScript.PedestriansValue = load._DATAPedestriansValue;
				MainMapScript.NoLeftTurnsValue = load._DATANoLeftTurnsValue;
				MainMapScript.InitialExecute();

			}

			else
			{
				MainMapScript.OneWays = false;
				MainMapScript.Pedestrians = false;
				MainMapScript.NoLeftTurns = false;

				MainMapScript.OneWaysValue = 0;
				MainMapScript.PedestriansValue = 0;
				MainMapScript.NoLeftTurnsValue = 0;
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

