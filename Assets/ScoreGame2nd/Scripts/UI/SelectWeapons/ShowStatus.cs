using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class StatusUIObj
{
	readonly int fontSize = 128;
	readonly Color32 smallestColor = new Color32(127, 0, 255, 255);
	readonly Color32 smallerColor = new Color32(0, 127, 255, 255);
	readonly Color32 normalColor = new Color32(127, 255, 0, 255);
	readonly Color32 higherColor = new Color32(255, 0, 127, 255);
	readonly Color32 highestColor = new Color32(255, 127, 0, 255);
	//TextMeshProUGUI nameText;
	TextMeshProUGUI explainText;
	public void SetComponents(/*TextMeshProUGUI name,*/ TextMeshProUGUI explain)
	{
		//nameText = name;
		explainText = explain;
	}
	//public void SetName(string name)
	//{
	//	nameText.text = name;
	//}
	public void SetExplanation(SizeEnum size)
	{
		switch (size)
		{
			case SizeEnum.verySmall:
				explainText.text = "��";
				explainText.color = smallestColor;
				explainText.fontSize = fontSize - 64;
				break;
			case SizeEnum.small:
				explainText.text = "��";
				explainText.color = smallerColor;
				explainText.fontSize = fontSize - 48;
				break;
			case SizeEnum.midium:
				explainText.text = "��";
				explainText.color = normalColor;
				explainText.fontSize = fontSize;
				break;
			case SizeEnum.big:
				explainText.text = "��";
				explainText.color = higherColor;
				explainText.fontSize = fontSize + 32;
				break;
			case SizeEnum.veryBig:
				explainText.text = "��";
				explainText.color = highestColor;
				explainText.fontSize = fontSize + 64;
				break;
			default:
				explainText.text = "?";
				explainText.color = normalColor;
				explainText.fontSize = fontSize;
				break;
		}
	}
	public void SetExplanation(SpeedEnum speed)
	{
		switch (speed)
		{
			case SpeedEnum.verySlow:
				explainText.text = "��";
				explainText.color = smallestColor;
				explainText.fontSize = fontSize - 64;
				break;
			case SpeedEnum.slow:
				explainText.text = "�x";
				explainText.color = smallerColor;
				explainText.fontSize = fontSize - 48;
				break;
			case SpeedEnum.midium:
				explainText.text = "��";
				explainText.color = normalColor;
				explainText.fontSize = fontSize;
				break;
			case SpeedEnum.fast:
				explainText.text = "��";
				explainText.color = higherColor;
				explainText.fontSize = fontSize + 32;
				break;
			case SpeedEnum.veryFast:
				explainText.text = "�u";
				explainText.color = highestColor;
				explainText.fontSize = fontSize + 64;
				break;
			default:
				explainText.text = "?";
				explainText.color = normalColor;
				explainText.fontSize = fontSize;
				break;

		}
	}
}
public class ShowStatus : MonoBehaviour
{
	List<GameObject> childs = new List<GameObject>();
	List<StatusUIObj> statusUIObjs = new List<StatusUIObj>();
	private void Start()
	{
		InitStatusUIObjs();
	}
	private void InitStatusUIObjs()
	{
		foreach (Transform child in transform)
		{
			StatusUIObj statusUIObj = new StatusUIObj();

			//TextMeshProUGUI name;
			//name = child.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

			TextMeshProUGUI explain;
			explain = child.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
			statusUIObj.SetComponents(explain);
			statusUIObjs.Add(statusUIObj);
		}
		InitChildList();
	}
	private void InitChildList()
	{
		if (childs.Count == 0)
		{
			foreach (Transform child in transform)
			{
				childs.Add(child.gameObject);
			}
		}
	}

	public void ShowAllStatusUI()
	{
		InitChildList();
		foreach (GameObject child in childs)
		{
			child.SetActive(true);
		}
	}
	public void HideAllStatusUI()
	{
		InitChildList();
		foreach (GameObject child in childs)
		{
			child.SetActive(false);
		}
	}
	public void SetTexts(EquipParam param)
	{
		ShowAllStatusUI();
		statusUIObjs[0].SetExplanation(param.damageInUI);
		statusUIObjs[1].SetExplanation(param.swingSpeedInUI);
		statusUIObjs[2].SetExplanation(param.readyForSwingSpeedInUI);
		statusUIObjs[3].SetExplanation(param.eatSpeedInUI);
		statusUIObjs[4].SetExplanation(param.healAmountInUI);
	}
}
