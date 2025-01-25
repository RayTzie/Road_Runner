using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobile.SnapDragon.Asyncsystem
{
	public class EngineScript : MonoBehaviour
	{
		int FrameRateSync = 60;
		bool Heat = true;
		float HeatCheck;
		float ResetHeatCheck = 30;
		// Start is called before the first frame update
		void Start()
		{
			Application.targetFrameRate = FrameRateSync;

			MobileSystemCheck(Heat);
		}

		void MobileSystemCheck(bool MobileSnapDragonID)
		{
			MobileSnapDragonID = Heat;


			if (Heat)
			{
				StartCoroutine("ReduceHeat");
			}
		

		}

		IEnumerator ReduceHeat()
		{
			yield return new WaitForSeconds(30);//20
			FrameRateSync -= 1;

			if(FrameRateSync <= 7)
			{
				FrameRateSync = 7;
			}
			Application.targetFrameRate = FrameRateSync;
			Heat = false;
			HeatCheck = ResetHeatCheck;
			StopCoroutine("ReduceHeat");
		}

		// Update is called once per frame
		void Update()
		{
			if (!Heat)
			{
				HeatCheck -= Time.deltaTime * 2.5f;

				if (HeatCheck <= 0)
				{ 
					HeatCheck = 0;
					Heat = true;
					MobileSystemCheck(Heat);
				}
			}

			
		}

		public void ConsoleFPS()
		{
			
		}
	}
}

