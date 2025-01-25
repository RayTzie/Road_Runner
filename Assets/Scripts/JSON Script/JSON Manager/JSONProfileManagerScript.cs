using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JSON.Database.Manager;
using Profile.Manager;

namespace JSON.System.Manager
{
	public class JSONProfileManagerScript : MonoBehaviour
	{
		[SerializeField]
		ProfileManagerScript MainProfileScript;
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
		JSONProfileDatabaseScript CreateData()
		{
			JSONProfileDatabaseScript save = new JSONProfileDatabaseScript();

			save._DATAMaleIcon = MainProfileScript.MaleIcon;
			save._DATAFemaleIcon = MainProfileScript.FemaleIcon;
			save._DATAProfileNameString = MainProfileScript.ProfileNameString;
			save._DATARoadTrafficSignValue = MainProfileScript.RoadTrafficSignValue;
			save._DATAPavementMarkingsValue = MainProfileScript.PavementMarkingsValue;
			save._DATADrivingOnTheRoadValue = MainProfileScript.DrivingOnTheRoadValue;
			save._DATATotalDriveValue = MainProfileScript.TotalDriveValue;
			save._DATAReadableEngagementValue = MainProfileScript.ReadableEngagementValue;

			return save;
		}

		void JSONSave()
		{
			JSONProfileDatabaseScript save = CreateData();

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

				JSONProfileDatabaseScript load = JsonUtility.FromJson<JSONProfileDatabaseScript>(JsonString);

				MainProfileScript.MaleIcon = load._DATAMaleIcon;
				MainProfileScript.FemaleIcon = load._DATAFemaleIcon;
				MainProfileScript.ProfileNameString = load._DATAProfileNameString;

				MainProfileScript.RoadTrafficSignValue = load._DATARoadTrafficSignValue;
				MainProfileScript.PavementMarkingsValue = load._DATAPavementMarkingsValue;
				MainProfileScript.DrivingOnTheRoadValue = load._DATADrivingOnTheRoadValue;
				MainProfileScript.TotalDriveValue = load._DATATotalDriveValue;
				MainProfileScript.ReadableEngagementValue = load._DATAReadableEngagementValue;

				MainProfileScript.InitialExecute();

			}

			else
			{
				MainProfileScript.MaleIcon = false;
				MainProfileScript.FemaleIcon = false;
				MainProfileScript.ProfileNameString = string.Empty;

				MainProfileScript.RoadTrafficSignValue = 100;
				MainProfileScript.PavementMarkingsValue = 100;
				MainProfileScript.DrivingOnTheRoadValue = 100;
				MainProfileScript.TotalDriveValue = 0;
				MainProfileScript.ReadableEngagementValue = 0;
				MainProfileScript.InitialExecute();
				JSONSave();
			}
		}

		// Update is called once per frame
		//void Update()
		//{

		//}
	}
}

