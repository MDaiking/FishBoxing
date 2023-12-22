using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionSetting : MonoBehaviour
{
	private TMP_Dropdown dropdown;
	[SerializeField]
	private TemporarySetting ts;
	private void Start()
	{
		dropdown = GetComponent<TMP_Dropdown>();
	}
	public void OnValueChanged(int value)
	{
		switch (value)
		{
			case 0://1920x1080
				ChangeResolution(1920, 1080);
				break;
			case 1://1366x768
				ChangeResolution(1366, 768);
				break;
			case 2://1280x720
				ChangeResolution(1280, 720);
				break;
		}
	}
	private void ChangeResolution(int x, int y)
	{
		ts.tempoSetting.Resolution = new Vector2Int(x, y);
	}
}
