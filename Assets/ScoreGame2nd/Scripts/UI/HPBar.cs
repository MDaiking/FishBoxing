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
			Debug.LogWarning(this + "��0�ŏ��Z���悤�Ƃ��܂���: HPBar");
			return;
		}
		hpbar.value = (float)hp / (float)maxhp;

	}
}
