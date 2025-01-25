using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSON.System.Manager;
using UnityEngine.UI;
using TMPro;

namespace Profile.Manager
{
	public class ProfileManagerScript : MonoBehaviour
	{
		[SerializeField]
		JSONProfileManagerScript JSONProfile;
		[Space]
		[Header("Profile Gender Settings")]
		public bool MaleIcon;
		public bool FemaleIcon;
		public string ProfileNameString;
		[SerializeField]
		Image[] ProfileImage;
		[SerializeField]
		TextMeshProUGUI[] ProfileNameText;
		[SerializeField]
		Sprite MaleSprite;
		[SerializeField]
		Sprite FemaleSprite;
		[SerializeField]
		GameObject ProfileObj;
		[SerializeField]
		Button[] GenderButton;
		[SerializeField]
		TMP_InputField ProfileNameInputField;

		[Space]
		[Header("Profile Status Settings")]
		public float RoadTrafficSignValue;
		public float PavementMarkingsValue;
		public float DrivingOnTheRoadValue;
		public float TotalDriveValue;
		public float ReadableEngagementValue;
		[SerializeField]
		float TotalPercentageDrivingValue;

		[Space]
		[Header("Profile Text Settings")]
		[SerializeField]
		TextMeshProUGUI RoadTrafficSignValueText;
		[SerializeField]
		TextMeshProUGUI PavementMarkingsValueText;
		[SerializeField]
		TextMeshProUGUI DrivingOnTheRoadValueText;
		[SerializeField]
		TextMeshProUGUI TotalDriveValueText;
		[SerializeField]
		TextMeshProUGUI ReadableEngagementValueText;
		[SerializeField]
		TextMeshProUGUI TotalPercentageDrivingValueText;

		[Space]
		[Header("Scene Settings")]
		[SerializeField]
		bool MainMenuScene;
		[SerializeField]
		bool GameplayScene;

		[Space]
		[Header("Gameplay Settings")]
		[SerializeField]
		TextMeshProUGUI TimerText;
		[SerializeField]
		float TempTimer;
		[SerializeField]
		float ActiveTimer;
		[SerializeField]
		float TempTotalDriveValue;
		public bool GameComplete;
		[SerializeField]
		GameObject MissionPassed;
		[SerializeField]
		GameObject MissionFailed;
		[SerializeField]
		SimpleCarController CarController;


		// Start is called before the first frame update
		void Start()
		{

		}

		public void InitialExecute()
		{
            if (MainMenuScene == true)
            {
				if (ProfileNameString == string.Empty)
				{
					ProfileObj.gameObject.SetActive(true);
				}

				else if (ProfileNameString != string.Empty)
				{
					ProfileObj.gameObject.SetActive(false);

					foreach (TextMeshProUGUI PNT in ProfileNameText)
					{
						PNT.text = ProfileNameString;
					}

					foreach (Image PI in ProfileImage)
					{
						if (MaleIcon)
						{
							PI.sprite = MaleSprite;
						}

						else if (FemaleIcon)
						{
							PI.sprite = FemaleSprite;
						}
					}
				}

				RoadTrafficSignValueText.text = RoadTrafficSignValue.ToString("F2") + "%";
				PavementMarkingsValueText.text = PavementMarkingsValue.ToString("F2") + "%";
				DrivingOnTheRoadValueText.text = DrivingOnTheRoadValue.ToString("F2") + "%";
				TotalDriveValueText.text = TotalDriveValue.ToString();
				ReadableEngagementValueText.text = ReadableEngagementValue.ToString("F2") + "%";

				float TempTotalValue = RoadTrafficSignValue + PavementMarkingsValue + DrivingOnTheRoadValue;
				TempTotalValue = Mathf.Round(TempTotalValue) / 3;

				TotalPercentageDrivingValue = TempTotalValue;

				TotalPercentageDrivingValueText.text = TotalPercentageDrivingValue.ToString("F2") + "%";
			}
           
			if (GameplayScene == true)
			{
				foreach (TextMeshProUGUI PNT in ProfileNameText)
				{
					PNT.text = ProfileNameString;
				}

				foreach (Image PI in ProfileImage)
				{
					if (MaleIcon)
					{
						PI.sprite = MaleSprite;
					}

					else if (FemaleIcon)
					{
						PI.sprite = FemaleSprite;
					}
				}

				RoadTrafficSignValueText.text = RoadTrafficSignValue.ToString("F2") + "%";
				PavementMarkingsValueText.text = PavementMarkingsValue.ToString("F2") + "%";
				DrivingOnTheRoadValueText.text = DrivingOnTheRoadValue.ToString("F2") + "%";
				TotalDriveValueText.text = TotalDriveValue.ToString();
				ReadableEngagementValueText.text = ReadableEngagementValue.ToString("F2") + "%";

				float TempTotalValue = RoadTrafficSignValue + PavementMarkingsValue + DrivingOnTheRoadValue;
				TempTotalValue = Mathf.Round(TempTotalValue) / 3;

				TotalPercentageDrivingValue = TempTotalValue;

				TotalPercentageDrivingValueText.text = TotalPercentageDrivingValue.ToString("F2") + "%";
			}
		}

		public void ProfileSettings(int ProfileID)
		{
			if (ProfileID == 0)
			{
				MaleIcon = true;
				FemaleIcon = false;
			}

			if (ProfileID == 1)
			{
				MaleIcon = false;
				FemaleIcon = true;
			}

			ProfileNameString = ProfileNameInputField.text;
			UpdateProfileSettings();
			JSONProfile.TempSave();
			ProfileObj.gameObject.SetActive(false);
		}

		void UpdateProfileSettings()
		{
			foreach (TextMeshProUGUI PNT in ProfileNameText)
			{
				PNT.text = ProfileNameString;
			}

			foreach (Image PI in ProfileImage)
			{
				if (MaleIcon)
				{
					PI.sprite = MaleSprite;
				}

				else if (FemaleIcon)
				{
					PI.sprite = FemaleSprite;
				}
			}
		}

		public void TempSave()
		{
			JSONProfile.TempSave();
		}

		public void ComputeProfileManualUpdate()
		{
			CarController.HandBrake = true;
			float TempTotalValue = RoadTrafficSignValue + PavementMarkingsValue + DrivingOnTheRoadValue;
			TempTotalValue = Mathf.Round(TempTotalValue) / 3;

			TotalPercentageDrivingValue = TempTotalValue;

			TotalDriveValue += TempTotalDriveValue;

			TotalPercentageDrivingValueText.text = TotalPercentageDrivingValue.ToString("F2") + "%";

			RoadTrafficSignValueText.text = RoadTrafficSignValue.ToString("F2") + "%";
			PavementMarkingsValueText.text = PavementMarkingsValue.ToString("F2") + "%";
			DrivingOnTheRoadValueText.text = DrivingOnTheRoadValue.ToString("F2") + "%";
			TotalDriveValueText.text = TotalDriveValue.ToString();
			ReadableEngagementValueText.text = ReadableEngagementValue.ToString("F2") + "%";

			if (TotalPercentageDrivingValue >= 80)
			{
				MissionPassed.gameObject.SetActive(true);
			}

			else if (TotalPercentageDrivingValue < 80)
			{
				MissionFailed.gameObject.SetActive(true);
			}

			TempSave();
		}

		public void ResetProfileData()
		{
			RoadTrafficSignValue = 100;
			PavementMarkingsValue = 100;
			DrivingOnTheRoadValue = 100;

			TotalPercentageDrivingValue = 100;

			TempSave();
		}

		// Update is called once per frame
		void Update()
		{
			if (MainMenuScene)
			{
				if (ProfileObj.activeSelf == true)
				{
					if (ProfileNameInputField.text != string.Empty)
					{
						foreach (Button GB in GenderButton)
						{
							GB.interactable = true;
						}
					}

					if (ProfileNameInputField.text == string.Empty)
					{
						foreach (Button GB in GenderButton)
						{
							GB.interactable = false;
						}
					}
				}
			}

			if (GameplayScene)
			{
				if (!GameComplete)
				{
					ActiveTimer += Time.deltaTime;
					TempTimer += Time.deltaTime;

					if (TempTimer >= 60)
					{
						TempTotalDriveValue += 1;
						TempTimer = 0;
					}

					TimerText.text = "Time: " + Mathf.Floor(ActiveTimer / 60).ToString("00") + ":" + Mathf.FloorToInt(ActiveTimer % 60).ToString("00");
				}
			}
		}
	}
}

