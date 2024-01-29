using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StartButton : ButtonInUI
{
	[SerializeField]
	private StartMenuController startMenu;
	[SerializeField]
	private TitleMenuController titleMenu;
	private TextMeshProUGUI tmpro;
	[SerializeField]
	private Color unselectedColor;
	[SerializeField]
	private Color selectedColor;

	private void Start()
	{
		tmpro = GetComponent<TextMeshProUGUI>();
		AddEventTrigger(new Action(() =>
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
		startMenu.ToggleStartMenuActive(true);
		titleMenu.ToggleTitleMenuActive(false);
	}
}
