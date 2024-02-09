using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GoToAnotherMenuButton : ButtonInUI
{
	[Header("�{�^�����������ۈڍs���郁�j���[���")]
	[SerializeField]
	private MenuController menuController;
	private MenuController myMenuController;
	private TextMeshProUGUI tmpro;
	[SerializeField]
	private Color unselectedColor;
	[SerializeField]
	private Color selectedColor;

	private void Start()
	{
		myMenuController = transform.parent.GetComponent<MenuController>();
		if (myMenuController == null)
		{
			Debug.LogError($"{transform.parent}��\"MenuController\"���R���|�[�l���g����Ă��܂���");
		}
		tmpro = GetComponent<TextMeshProUGUI>();
		AddEventTrigger(
			new Action(() =>
			{
				tmpro.color = selectedColor;
			}),
			new Action(() =>
			{
				tmpro.color = unselectedColor;
			}),
			new Action(() =>
			{
				SceneToStartMenu();
			}));
	}
	private void SceneToStartMenu()
	{
		menuController.ToggleMenuActive(true);
		myMenuController.ToggleMenuActive(false);
	}
}
