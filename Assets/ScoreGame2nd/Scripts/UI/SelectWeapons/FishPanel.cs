using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform))]
public class FishPanel : ButtonInUI
{
	private SelectWeaponParent selectWeaponParent;
	private RectTransform rtransform;
	private AboutWeaponController aboutWeaponController;
	private float width;
	private float height;
	[SerializeField]
	private EquipLists equipLists;
	[SerializeField]
	private int fishNum;
	private Image panel;
	private Image image;
	[SerializeField]
	private Color unselectedPanelColor;
	[SerializeField]
	private Color selectedPanelColor;

	private void Start()
	{
		rtransform = GetComponent<RectTransform>();
		panel = GetComponent<Image>();
		selectWeaponParent = GameObject.FindWithTag("SelectWeapons").GetComponent<SelectWeaponParent>();
		aboutWeaponController = GameObject.FindWithTag("AboutWeapon").GetComponent<AboutWeaponController>();
		image = transform.GetChild(0).GetComponent<Image>();
		SetEnable(false);
		AddEventTrigger(new Action(() => { if (!isSelected) { EnterToMe(); } }),
			new Action(() => { if (!isSelected) { ExitFromMe(); } }),
			new Action(() => { ClickMe(); }));
		image.sprite = equipLists.equipParamList[fishNum].equipImage;

		width = 1280.0f / 5.0f;
		height = rtransform.sizeDelta.y;
		rtransform.sizeDelta = new Vector2(width, height);
	}
	public void SetEnable(bool enable)
	{
		isSelected = enable;
		if (enable)
		{

		}
		else
		{
			ExitFromMe();
		}
	}
	public void EnterToMe()
	{
		panel.color = selectedPanelColor;
		aboutWeaponController.SetWeaponStatus(fishNum);
	}
	private void ExitFromMe()
	{
		panel.color = unselectedPanelColor;
		aboutWeaponController.SetWeaponStatus(-1);
	}
	private void ClickMe()
	{
		PlayerEquips playerEquips = selectWeaponParent.PlayerEquip;
		if (playerEquips != null)
		{
			playerEquips.ResetPlayerEquip(fishNum);
			selectWeaponParent.ToggleUI(false);
			SetEnable(true);
			aboutWeaponController.SetNowSelectFish(fishNum);
			panel.color = selectedPanelColor;
		}
	}
}
