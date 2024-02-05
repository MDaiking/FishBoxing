using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StartClientButton : ButtonInUI
{
	[SerializeField]
	private GameManager gameManager;
	[SerializeField]
	private TMP_InputField inputField;
	[SerializeField]
	private LoadingController loadingController;
	[SerializeField]
	private string multiBattleScene;
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
				StartClient();
			}));
	}
	private void StartClient()
	{
		string clientip = inputField.text;

	}
}
