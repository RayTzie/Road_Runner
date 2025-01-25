using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IntroManager
{
	public class IntroManagerScript : MonoBehaviour
	{
		[SerializeField]
		string SceneName;
		[SerializeField]
		float LoadTimer;
		// Start is called before the first frame update
		void Start()
		{
			LoadScene();
		}

		void LoadScene()
		{
			StartCoroutine(LoadSceneDelay());
		}

		IEnumerator LoadSceneDelay()
		{
			yield return new WaitForSeconds(LoadTimer);
			SceneManager.LoadScene(SceneName);
		}


		// Update is called once per frame
		void Update()
		{

		}
	}
}

