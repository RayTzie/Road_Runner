using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornScript : MonoBehaviour
{
	[SerializeField]
	float HornTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnEnable()
	{
		StartCoroutine("DisableHorn");
	}

	IEnumerator DisableHorn()
	{
		yield return new WaitForSeconds(HornTimer);
		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
