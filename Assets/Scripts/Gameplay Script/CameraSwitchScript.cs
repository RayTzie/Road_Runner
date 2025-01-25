using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
	[SerializeField]
	Transform[] CameraPosition;
	[SerializeField]
	GameObject Camera;
	int CameraView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Camera.transform.position = CameraPosition[CameraView].position;
	}

	public void AdjustCamera()
	{
		if (CameraView == 0)
		{
			CameraView = 1;
		}

		else if (CameraView
			== 1)
		{
			CameraView = 0;
		}
	}
}
