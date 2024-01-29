using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class PracticeButton : ButtonInUI
{
	private TextMeshProUGUI tmpro;
	[SerializeField]
	private Color unselectedColor;
	[SerializeField]
	private Color selectedColor;
	[SerializeField]
	private LoadingController loadingController;
	[SerializeField]
	private string practiceSceneName;
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
				loadingController.LoadNextScene(practiceSceneName);
			}));
	}
}
