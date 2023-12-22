using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySetting : MonoBehaviour
{
	[SerializeField]
	private PlayerSetting playerSetting;
	[SerializeField]
	private PauseParent pauseParent;
	public PlayerSettingData tempoSetting;

	private void Start()
	{
		tempoSetting = new PlayerSettingData();
	}

	public void ChangeAllSettingToPS()
	{
		tempoSetting = playerSetting.settingData;
	}
	public void ApplySettings()
	{
		playerSetting.settingData.Save(tempoSetting);
		playerSetting.ChangeSettings();
		pauseParent.InactiveSetting();
	}
	public void ResetSettings()
	{
		tempoSetting.Initialize();
		pauseParent.InactiveSetting();
	}
}
