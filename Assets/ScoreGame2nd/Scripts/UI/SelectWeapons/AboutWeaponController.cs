using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AboutWeaponController : MonoBehaviour
{
	[SerializeField]
	private Sprite nullImage;
	private Color nullImageColor;
	[SerializeField]
	private List<FishPanel> fishPanels = new List<FishPanel>();
	[SerializeField]
	private EquipLists equipLists;
	[SerializeField]
	private Image image;
	[SerializeField]
	private TextMeshProUGUI ruby;
	[SerializeField]
	new private TextMeshProUGUI name;
	[SerializeField]
	private TextMeshProUGUI explanation;
	[SerializeField]
	private ShowStatus status;
	private int nowSelectFish = -1;

	int showWeaponNum;
	private void Start()
	{
		showWeaponNum = -1;
		SetWeaponStatus(-1);
		foreach (Transform child in transform.parent)
		{
			FishPanel fishPanel = child.GetComponent<FishPanel>();
			if (fishPanel != null)
			{
				fishPanels.Add(fishPanel);
			}
		}
		nullImageColor = GetComponent<Image>().color;
	}
	private void Update()
	{

	}
	public void SetNowSelectFish(int num)
	{
		nowSelectFish = num;
		SetSelectFish(nowSelectFish);
	}
	public void SetWeaponStatus(int num)
	{
		if (num == -1)
		{
			if (nowSelectFish == -1)
			{
				image.sprite = nullImage;
				image.color = nullImageColor;
				ruby.text = "";
				name.text = "";
				explanation.text = "";
				status.HideAllStatusUI();
			}
			else
			{
				SetWeaponStatus(nowSelectFish);
			}
		}
		else
		{
			EquipParam equipParam = equipLists.equipParamList[num];
			image.sprite = equipParam.equipImage;
			image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			ruby.text = equipParam.ruby;
			name.text = equipParam.name;
			explanation.text = equipParam.explanation;
			status.SetTexts(equipParam);

		}
	}
	private void SetSelectFish(int num)
	{
		for (int i = 0; i < fishPanels.Count; ++i)
		{
			fishPanels[i].SetEnable(i == num);
		}
	}
}
