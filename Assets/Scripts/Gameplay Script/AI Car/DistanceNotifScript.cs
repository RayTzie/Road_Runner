using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Notification
{
	public class DistanceNotifScript : MonoBehaviour
	{
		[SerializeField]
		GameObject NotifObj;
		[SerializeField]
		bool LastNotification;
		// Start is called before the first frame update
		void Start()
		{

		}

		private void OnEnable()
		{
			if (LastNotification)
			{
				NotifObj.gameObject.SetActive(false);
			}
			StartCoroutine(HideNotification());
		}

		IEnumerator HideNotification()
		{
			yield return new WaitForSeconds(1.8f);
			this.gameObject.SetActive(false);
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}

