using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]

public class CheckEatingController : MonoBehaviour
{
	RectTransform rectTransform;
	private float defaultWeight;
	private float maskPercent;
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

		defaultWeight = rectTransform.rect.width;
	}
	private void Update()
	{
		 
	}

	public void SetFishMaskPercent(float percent)
	{
		maskPercent = percent;
	}
	private void SetFishSprite(EquipParam _equipParam)
	{
		equipParam = _equipParam;
		fishImage.sprite = equipParam.equipImage;
	}
	public void SetFishColor(Color color)
	{
		fishColor.color = color;
	}
}
