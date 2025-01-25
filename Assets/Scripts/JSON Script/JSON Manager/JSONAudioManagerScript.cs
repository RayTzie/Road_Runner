using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JSON.Database.Manager;
using Audio.Manager;

namespace JSON.System.Manager
{
	public class JSONAudioManagerScript : MonoBehaviour
	{
		[SerializeField]
		AudioManagerScript MainAudioScript;
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
		JSONAudioDatabaseScript CreateData()
		{
			JSONAudioDatabaseScript save = new JSONAudioDatabaseScript();

			save._DATAMusicVolume = MainAudioScript.MusicVolume;
			save._DATASoundVolume = MainAudioScript.SoundVolume;

			return save;
		}

		void JSONSave()
		{
			JSONAudioDatabaseScript save = CreateData();

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

				JSONAudioDatabaseScript load = JsonUtility.FromJson<JSONAudioDatabaseScript>(JsonString);

				MainAudioScript.MusicVolume = load._DATAMusicVolume;
				MainAudioScript.SoundVolume = load._DATASoundVolume;
				MainAudioScript.InitialExecute();

			}

			else
			{
				MainAudioScript.MusicVolume = 1;
				MainAudioScript.SoundVolume = 1;
				MainAudioScript.InitialExecute();
				JSONSave();
			}
		}

		// Update is called once per frame
		//void Update()
		//{

		//}
	}
}

