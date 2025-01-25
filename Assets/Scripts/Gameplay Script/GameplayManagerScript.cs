using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Profile.Manager;
//using UnityEditor.SearchService;

namespace Gameplay.Manager
{
	public class GameplayManagerScript : MonoBehaviour
	{
		[SerializeField]
		ProfileManagerScript MainProfile;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void LoadScene(string SceneID)
		{
			SceneManager.LoadScene(SceneID);
		}

		public void FailedLoadScene(string SceneID)
		{
			MainProfile.ResetProfileData();
			StartCoroutine(ReloadScene(SceneID));
			
		}

		IEnumerator ReloadScene(string SceneTempID)
		{
			yield return new WaitForSeconds(.01f);
			SceneManager.LoadScene(SceneTempID);
		}

		public void ShowObject(GameObject ObjectID)
		{
			ObjectID.SetActive(true);
		}

		public void HideObject(GameObject ObjectID)
		{
			ObjectID.SetActive(false); 
		}

	}
}

