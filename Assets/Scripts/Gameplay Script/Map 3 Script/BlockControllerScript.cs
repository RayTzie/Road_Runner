using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block.Controller
{
	public class BlockControllerScript : MonoBehaviour
	{
		[SerializeField]
		GameObject[] DisableObj;
		[SerializeField]
		GameObject[] EnableObj;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnTriggerStay(Collider col)
		{
			Debug.Log(col.gameObject.tag);
			if (col.CompareTag("Player"))
			{
				SwitchObj();
			}
		}

		void SwitchObj()
		{
			foreach (GameObject DO in DisableObj)
			{
				if (DO != null)
				{
					DO.gameObject.SetActive(false);
				}
				
			}

			foreach (GameObject EO in EnableObj)
			{
				if (EO != null)
				{
					EO.gameObject.SetActive(true);
				}
			}
		}
	}
}

