using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StartHostButton : ButtonInUI
{
	[SerializeField]
	private GameManager gameManager;
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
				StartHost();
			}));
	}
	private void StartHost()
	{
		SceneManager.LoadScene("ShaderTest");
	}
}
