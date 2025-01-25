using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map.Manager;
using TMPro;
using Profile.Manager;
using Mobile.AsyncAndroid;

namespace Deduction.Manager
{
	public class DeductionManagerScript : MonoBehaviour
	{
		[Header("Map Settings")]
		[SerializeField]
		bool VillageIgnacioMapActivate;
		[SerializeField]
		bool MayorJaldonStreetMapActivate;
		[SerializeField]
		bool CentroPuebloMapActivate;

		[Space]
		[Header("Profile Settings")]
		[SerializeField]
		ProfileManagerScript MainProfile;
		[SerializeField]
		GameObject ProfileBackButton;
		[SerializeField]
		GameObject ProfileObj;

		[Space]
		[Header("Village Ignacio Settings")]
		[SerializeField]
		VillageIgnacioFillInvestManagerScript VillageIgnacioMap;
		

		[Space]
		[Header("Speed Limit Settings")]
		public float SpeedLimit;
		[SerializeField]
		float SpeedLimitDeductionValue;
		public bool OverSpeedActivate;
		[SerializeField]
		float WrongWayDeductionValue;
		[SerializeField]
		float TotalDeductionPoints;
		[SerializeField]
		TextMeshProUGUI TotalDeductionValueText;
		[SerializeField]
		Color DeductionColor;
		[SerializeField]
		Color NonDeductionColor;
		bool HitWall;

		[Space]
		[Header("Mayor Jaldon Street Settings")]
		[SerializeField]
		MayorJaldonManagerScript MayorJaldonStreetMap;
		[SerializeField]
		float TrafficLightDeductionValue;
		[SerializeField]
		float UnSafeLaneChangesViolationValue;
		[SerializeField]
		float FailuretoYieldValue;

		[Space]
		[Header("Centro Pueblo Street Settings")]
		[SerializeField]
		CentroPuebloManagerScript CentroPuebloMap;
		[SerializeField]
		float OneWaysDeductionValue;
		[SerializeField]
		float PedestriansDeductionValue;
		[SerializeField]
		float LegalTurnDeductionValue;
		AndroidSnapSystem AsyncSnapDragon;


		// Start is called before the first frame update
		void Start()
		{
			AsyncSnapDragon = GameObject.FindObjectOfType<AndroidSnapSystem>();

			if (AsyncSnapDragon == null)
			{
				Destroy(this.gameObject);
			}

			TotalDeductionValueText.text = "Deduction Points: "+ TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = NonDeductionColor;
		}

		public void OneWaysActivate()
		{
			HitWall = true;
			TotalDeductionPoints += OneWaysDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void PedestriansActivate()
		{
			HitWall = true;
			TotalDeductionPoints += PedestriansDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void LegalTurnActivate()
		{
			HitWall = true;
			TotalDeductionPoints += LegalTurnDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void WrongWayDirectionActivate()
		{
			HitWall = true;
			TotalDeductionPoints += WrongWayDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}
		public void OvertakingDirectionActivate()
		{
			HitWall = true;
			TotalDeductionPoints += WrongWayDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void TrafficLightViolationActivate()
		{
			HitWall = true;
			TotalDeductionPoints += TrafficLightDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void UnsafeLaneChangeActivate()
		{
			HitWall = true;
			TotalDeductionPoints += UnSafeLaneChangesViolationValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void VehicleHitActivate()
		{
			HitWall = true;
			TotalDeductionPoints += FailuretoYieldValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}

		public void PedestrianHitActivate()
		{
			HitWall = true;
			TotalDeductionPoints += PedestriansDeductionValue;
			TotalDeductionValueText.color = DeductionColor;
			TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
			TotalDeductionValueText.color = DeductionColor;
			StopCoroutine("ChangeTextColor");
			StartCoroutine("ChangeTextColor");
		}


		IEnumerator ChangeTextColor()
		{
			yield return new WaitForSeconds(.3f);
			HitWall = false;
			TotalDeductionValueText.color = NonDeductionColor;
		}

		public void DeductData()
		{
			if (VillageIgnacioMapActivate)
			{
				MainProfile.GameComplete = true;
				if (VillageIgnacioMap.SpeedLimit)
				{
					VillageIgnacioMap.SpeedLimitValue = 1;
					VillageIgnacioMap.TempSave();
					MainProfile.RoadTrafficSignValue -= TotalDeductionPoints;

					if (MainProfile.RoadTrafficSignValue <= 0)
					{
						MainProfile.RoadTrafficSignValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}


				if (VillageIgnacioMap.Turns)
				{
					VillageIgnacioMap.TurnsValue = 12;
					VillageIgnacioMap.TempSave();
					MainProfile.PavementMarkingsValue -= TotalDeductionPoints;

					if (MainProfile.PavementMarkingsValue <= 0)
					{
						MainProfile.PavementMarkingsValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}

				if (VillageIgnacioMap.OverTaking)
				{
					VillageIgnacioMap.OverTakingValue = 1;
					VillageIgnacioMap.TempSave();
					MainProfile.DrivingOnTheRoadValue -= TotalDeductionPoints;

					if (MainProfile.DrivingOnTheRoadValue <= 0)
					{
						MainProfile.DrivingOnTheRoadValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}
			}

			if (MayorJaldonStreetMapActivate)
			{
				MainProfile.GameComplete = true;
				if (MayorJaldonStreetMap.UTurns)
				{
					MayorJaldonStreetMap.UTurnsValue = 3;
					MayorJaldonStreetMap.TempSave();
					MainProfile.RoadTrafficSignValue -= TotalDeductionPoints;

					if (MainProfile.RoadTrafficSignValue <= 0)
					{
						MainProfile.RoadTrafficSignValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}

				if (MayorJaldonStreetMap.VehicleYielding)
				{
					MayorJaldonStreetMap.VehicleYieldingValue = 1;
					MayorJaldonStreetMap.TempSave();
					MainProfile.DrivingOnTheRoadValue -= TotalDeductionPoints;

					if (MainProfile.DrivingOnTheRoadValue <= 0)
					{
						MainProfile.DrivingOnTheRoadValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}

				if (MayorJaldonStreetMap.Skyway)
				{
					MayorJaldonStreetMap.SkywayValue = 2;
					MayorJaldonStreetMap.TempSave();
					MainProfile.DrivingOnTheRoadValue -= TotalDeductionPoints;

					if (MainProfile.DrivingOnTheRoadValue <= 0)
					{
						MainProfile.DrivingOnTheRoadValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}
			}

			if (CentroPuebloMapActivate)
			{
				MainProfile.GameComplete = true;
				if (CentroPuebloMap.OneWays)
				{
					CentroPuebloMap.OneWaysValue = 3;
					CentroPuebloMap.TempSave();
					MainProfile.RoadTrafficSignValue -= TotalDeductionPoints;

					if (MainProfile.RoadTrafficSignValue <= 0)
					{
						MainProfile.RoadTrafficSignValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}

				if (CentroPuebloMap.Pedestrians)
				{
					CentroPuebloMap.PedestriansValue = 2;
					CentroPuebloMap.TempSave();
					MainProfile.RoadTrafficSignValue -= TotalDeductionPoints;

					if (MainProfile.PavementMarkingsValue <= 0)
					{
						MainProfile.PavementMarkingsValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}

				if (CentroPuebloMap.NoLeftTurns)
				{
					CentroPuebloMap.NoLeftTurnsValue = 2;
					CentroPuebloMap.TempSave();
					MainProfile.RoadTrafficSignValue -= TotalDeductionPoints;

					if (MainProfile.RoadTrafficSignValue <= 0)
					{
						MainProfile.RoadTrafficSignValue = 0;
					}

					MainProfile.ComputeProfileManualUpdate();

					ProfileBackButton.gameObject.SetActive(false);
					ProfileObj.gameObject.SetActive(true);
				}
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (OverSpeedActivate)
			{
				TotalDeductionPoints += SpeedLimitDeductionValue;
				TotalDeductionValueText.text = "Deduction Points: " + TotalDeductionPoints.ToString("F2");
				TotalDeductionValueText.color = DeductionColor;
			}

			if (!OverSpeedActivate)
			{
				if (!HitWall)
				{
					TotalDeductionValueText.color = NonDeductionColor;
				}
				
			}
		}
	}
}

