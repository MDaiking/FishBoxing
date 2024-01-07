using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]

public class PauseButton : ButtonInUI
{
	private PauseParent pauseParent;
	private TextMeshProUGUI text;
	[SerializeField]
	private Color selectedColor;
	[SerializeField]
	private Color unselectedColor;
	[SerializeField]
	private Color activeColor;
	private int pauseButtonNum;
	public int PauseButtonNum
	{
		get { return pauseButtonNum; }
		set { pauseButtonNum = value; }
	}

	[SerializeField]
	private List<GameObject> children;
	private void Start()
	{
		foreach (Transform child in transform)
		{
			children.Add(child.gameObject);
		}

		pauseParent = transform.parent.GetComponent<PauseParent>();
		text = GetComponent<TextMeshProUGUI>();

		AddEventTrigger(new Action(() => { if (!isSelected) text.color = selectedColor; }),
			new Action(() => { if (!isSelected) text.color = unselectedColor; }),
			new Action(() => { pauseParent.ActiveSetting(pauseButtonNum); }));
	}

	public void SetEnable(bool enable)
	{
		if (text == null) text = GetComponent<TextMeshProUGUI>();
		isSelected = enable;
		if (enable)
		{
			text.color = activeColor;
			SetActiveAllChildren(true);
		}
		else
		{
			text.color = unselectedColor;
			SetActiveAllChildren(false);
		}
	}

	private void SetActiveAllChildren(bool activice)
	{
		foreach (GameObject child in children)
			child.SetActive(activice);
	}
}
