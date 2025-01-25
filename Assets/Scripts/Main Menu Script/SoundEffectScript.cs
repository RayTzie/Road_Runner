using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio.SoundEffect
{
	public class SoundEffectScript : MonoBehaviour
	{
		public AudioClip ButtonSoundEffect;

		[SerializeField]
		AudioSource SoundEffectAudio;
		// Start is called before the first frame update
		void Start()
		{

		}

		public void PlaySoundEffectAudio(AudioClip clip)
		{
			SoundEffectAudio.PlayOneShot(clip);
		}
	}
}

