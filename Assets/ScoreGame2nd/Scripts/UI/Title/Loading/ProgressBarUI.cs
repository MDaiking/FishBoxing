using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{
    private float width;
	private RectTransform childRT;
	private Vector2 childSizeDelta;
	private void Start()
	{
		width = GetComponent<RectTransform>().sizeDelta.x;
		childRT = transform.GetChild(0).GetComponent<RectTransform>();
		childSizeDelta = childRT.sizeDelta;
	}
	
	public void SetPercentage(float percent)
	{
		childSizeDelta.x = width * percent;
		childRT.sizeDelta = childSizeDelta;
	}
}
