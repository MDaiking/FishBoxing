using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : ButtonInUI
{
	[SerializeField]
	private CreditController creditController;
	[SerializeField]
	private TitleMenuController titleButtons;

	private bool isActive;
	private void Start()
	{
		isActive = false;
	}

	private void ToggleCredit()
	{
		isActive = !isActive;
		if (isActive)
		{
			titleButtons.ToggleTitleMenuActive(false);
		}
	}
}
