using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
	private Scrollbar hpbar;
	private void Start()
	{
		hpbar = GetComponent<Scrollbar>();
	}
	public void ChangeHPBar(int hp, int maxhp)
	{
		if (maxhp == 0)
		{
			Debug.LogWarning(this + "が0で除算しようとしました: HPBar");
			return;
		}
		hpbar.value = (float)hp / (float)maxhp;

	}
}
