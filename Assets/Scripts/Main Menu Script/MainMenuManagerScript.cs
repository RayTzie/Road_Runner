using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio.SoundEffect;
using Mobile.AsyncAndroid;

namespace MainMenu.Manager
{
	public class MainMenuManagerScript : MonoBehaviour
	{
		[SerializeField]
		SoundEffectScript MainSFX;
		AndroidSnapSystem AsyncSnapDragon;
		// Start is called before the first frame update
		void Start()
		{
			AsyncSnapDragon = GameObject.FindObjectOfType<AndroidSnapSystem>();

			if (AsyncSnapDragon == null)
			{
				Destroy(this.gameObject);
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void DisplayObject(GameObject ObjID)
		{
			ObjID.gameObject.SetActive(true);
		}

		public void HideObject(GameObject ObjID)
		{
			ObjID.gameObject.SetActive(false);
		}

		public void SoundEffect()
		{
			MainSFX.PlaySoundEffectAudio(MainSFX.ButtonSoundEffect);
		}
	}
}

