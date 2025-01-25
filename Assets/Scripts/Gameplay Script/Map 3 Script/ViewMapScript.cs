using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViewMap
{
	public class ViewMapScript : MonoBehaviour
	{
		[SerializeField]
		GameObject[] MiniMapFullViewObj;

		[SerializeField]
		GameObject[] MiniMapPartialViewObj;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void PressedMap()
		{
			foreach(GameObject MMFVO in MiniMapFullViewObj)
			{
				MMFVO.gameObject.SetActive(true);
			}

			foreach (GameObject SMMPVO in MiniMapPartialViewObj)
			{
				SMMPVO.gameObject.SetActive(false);
			}
		}

		public void ReleaseMap()
		{
			foreach (GameObject MMFVO in MiniMapFullViewObj)
			{
				MMFVO.gameObject.SetActive(false);
			}

			foreach (GameObject SMMPVO in MiniMapPartialViewObj)
			{
				SMMPVO.gameObject.SetActive(true);
			}
		}
	}
}

