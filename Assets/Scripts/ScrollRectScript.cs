using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectScript : MonoBehaviour
{
	[SerializeField]
	ScrollRect scrollRect;
	[SerializeField]
	float minYPosition = 0f; // Minimum scroll limit
	[SerializeField]
	float maxYPosition = 500f; // Maximum scroll limit
									  // Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 contentPosition = scrollRect.content.anchoredPosition;
		contentPosition.y = Mathf.Clamp(contentPosition.y, minYPosition, maxYPosition);
		scrollRect.content.anchoredPosition = contentPosition;
	}
}
