using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class StatusUIObj
{
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
	public void SetExplanation(string explanation)
	{
		explainText.text = explanation;
	}
}
public class ShowStatus : MonoBehaviour
{
	List<GameObject> childs;
	List<StatusUIObj> statusUIObjs;
	private void Start()
	{
		InitStatusUIObjs();
	}
	private void InitStatusUIObjs()
	{
		foreach (Transform child in transform)
		{
			childs.Add(child.gameObject);
			StatusUIObj statusUIObj = new StatusUIObj();

			//TextMeshProUGUI name;
			//name = child.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

			TextMeshProUGUI explain;
			explain = child.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
			statusUIObj.SetComponents(explain);
			statusUIObjs.Add(statusUIObj);
		}
	}

	public void ShowAllStatusUI()
	{
		foreach(GameObject child in childs)
		{
			child.SetActive(true);
		}
	}
	public void HideAllStatusUI()
	{
		foreach (GameObject child in childs)
		{
			child.SetActive(false);
		}
	}
	public void SetTexts(List<string> explanation)
	{
		int statusUIAmount = statusUIObjs.Count;
		if (statusUIAmount != explanation.Count)
		{
			Debug.LogError("ShowStatus: à¯êîÇ™ÅA" + statusUIAmount + "å¬Ç≈ÇÕÇ†ÇËÇ‹ÇπÇÒ");
			return;
		}

		for (int i = 0; i < statusUIAmount; ++i)
		{
			//statusUIObjs[i].SetName(name[i]);
			statusUIObjs[i].SetExplanation(explanation[i]);
		}
	}
}
