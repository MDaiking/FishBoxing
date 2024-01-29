using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParent : MonoBehaviour
{
	[SerializeField, Tooltip("ポーズ画面を出したときにまず表示される設定画面")]
	private int displayAtActive = 0;

	[SerializeField]
	private List<GameObject> settings;
	private GameObject mainSetting;
	[SerializeField]
	private List<PauseButton> settingsPB;

	private CursorController cursorController;
	private SystemInputList sil;

	private bool isPause;
	private void Start()
	{
		bool isFirst = false;
		foreach (Transform child in transform)
		{
			if (!isFirst)//最初の子オブジェクトは常時表示するのでスキップ
			{
				mainSetting = child.gameObject;
				isFirst = true;
				continue;
			}
			settings.Add(child.gameObject);
			settingsPB.Add(child.GetComponent<PauseButton>());
		}
		isPause = false;

		cursorController = GameObject.FindWithTag("GameController").GetComponent<CursorController>();
		sil = GameObject.FindWithTag("GameManager").GetComponent<SystemInputList>();
		SetNumInSettings();
		InactiveSetting();
	}
	private void Update()
	{
		if (sil.IsOptionStart)
		{
			isPause = !isPause;
			if (isPause) ActiveSetting(displayAtActive);
			else InactiveSetting();
		}
	}
	//numの設定項目をactiveする
	public void ActiveSetting(int num)
	{
		isPause = true;
		ActiveAllSettings(true);
		for (int i = 0; i < settingsPB.Count; ++i)
		{
			if (i == num) settingsPB[i].SetEnable(true);
			else settingsPB[i].SetEnable(false);
		}
		cursorController.IsCursorShow = true;
	}
	public void InactiveSetting()
	{
		isPause = false;
		ActiveAllSettings(false);
		cursorController.IsCursorShow = false;
	}

	//設定の数字をつける
	private void SetNumInSettings()
	{
		int i = 0;
		foreach (PauseButton settingPB in settingsPB)
		{
			if (i == 0) settingPB.SetEnable(true);
			else settingPB.SetEnable(false);
			settingPB.PauseButtonNum = i++;
		}
	}

	private void ActiveAllSettings(bool activate)
	{
		mainSetting.SetActive(activate);
		foreach (GameObject setting in settings)
			setting.SetActive(activate);
	}
}
