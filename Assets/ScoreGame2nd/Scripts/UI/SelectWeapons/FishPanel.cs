using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishPanel : ButtonInUI
{
	[SerializeField]
	int fishNum;
	EventTrigger eventTrigger;

	private void Start()
	{
		AddEventTrigger(new Action(() => { if (!isSelected) ; }),
			new Action(() => { if (!isSelected) ; }),
			new Action(() => { SelectMe(); }));
	}
	public void SetEnable(bool enable)
	{
		isSelected = enable;
		if (enable)
		{

		}
		else
		{

		}
	}

	void SelectMe()
	{

	}
}
