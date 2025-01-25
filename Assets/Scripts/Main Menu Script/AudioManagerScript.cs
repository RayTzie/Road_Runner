using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSON.System.Manager;

namespace Audio.Manager
{
	public class AudioManagerScript : MonoBehaviour
	{
		[SerializeField]
		JSONAudioManagerScript JSONAudio;
		[SerializeField]
		Slider MusicSlider;
		[SerializeField]
		Slider SoundSlider;
		[SerializeField]
		AudioSource MusicSource;
		[SerializeField]
		AudioSource SoundSource;
		public float MusicVolume;
		public float SoundVolume;

		[Space]
		[Header("Scene Settings")]
		[SerializeField]
		bool MainMenuScene;
		[SerializeField]
		bool GameplayMenuScene;

		[Space]
		[Header("Gameplay Settings")]
		[SerializeField]
		AudioSource MusicGameplay;
		[SerializeField]
		AudioSource[] SoundGameplay;
		[SerializeField]
		GameObject HornObj;

		// Start is called before the first frame update
		void Start()
		{

		}

		public void InitialExecute()
		{
			if (MainMenuScene == true)
			{
				MusicSource.volume = MusicVolume;
				MusicSlider.value = MusicVolume;
				SoundSource.volume = SoundVolume;
				SoundSlider.value = SoundVolume;
			}

			if (GameplayMenuScene == true)
			{
				MusicGameplay.volume = MusicVolume;

				foreach(AudioSource SG in SoundGameplay)
				{
					SG.volume = SoundVolume;
				}
			}
			
		}

		public void AdjustAudio(int AudioID)
		{
			if (AudioID == 0)
			{
				MusicVolume = MusicSlider.value;
				MusicSource.volume = MusicSlider.value;
			}

			if (AudioID == 1)
			{
				SoundVolume = SoundSlider.value;
				SoundSource.volume = SoundSlider.value;
			}
		}

		public void SaveAudioData()
		{
			JSONAudio.TempSave();
		}

		public void ActivateHorn()
		{
			if (HornObj.activeSelf == false)
			{
				HornObj.gameObject.SetActive(true);
			}
		}

		// Update is called once per frame
		//void Update()
		//{

		//}
	}
}

