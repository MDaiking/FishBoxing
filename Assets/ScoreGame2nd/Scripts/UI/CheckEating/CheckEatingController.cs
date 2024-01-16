using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]

public class CheckEatingController : MonoBehaviour
{
	RectTransform rectTransform;
	private float maskPercent;
	private Vector2 defaultSize;
	private float width;
	[SerializeField]
	private float runningBackSpeed = 5f;
	private EquipParam equipParam;
	private Image fishImage;
	private Image fishColor;
	[SerializeField]
	private Color canEatColor;
	[SerializeField]
	private Color cannotEatColor; 
	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		Transform fishImageTransform = transform.GetChild(0);
		fishImage = fishImageTransform.GetComponent<Image>();
		fishColor = fishImageTransform.GetChild(0).GetComponent<Image>();
		defaultSize = rectTransform.sizeDelta;
		maskPercent = 1.0f;
	}
	private void Update()
	{
		float targetWidth = defaultSize.x * maskPercent;
		if (targetWidth - runningBackSpeed > width)
		{
			width += runningBackSpeed;
		}
		else
		{
			width = targetWidth;
		}
		rectTransform.sizeDelta = new Vector2(width, defaultSize.y);
	}

	public void SetFishMaskPercent(float percent)
	{
		maskPercent = 1.0f - percent;
	}
	public void SetEatingFishSprite(EquipParam _equipParam)
	{
		equipParam = _equipParam;
		fishImage.sprite = equipParam.equipImage;
	}
	public void SetFishColor(Color color)
	{
		fishColor.color = color;
	}
}
