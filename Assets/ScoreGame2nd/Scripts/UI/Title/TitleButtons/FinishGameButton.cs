using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]

public class FinishGameButton : ButtonInUI
{
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
				EndGame();
			}));
	}
	private void EndGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
	}
}
