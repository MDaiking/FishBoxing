using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PauseButton : MonoBehaviour
{
	private PauseParent pauseParent;
	private EventTrigger eventTrigger;
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

	private bool isActive;
	[SerializeField]
	private List<GameObject> children;
	private void Start()
	{
		foreach (Transform child in transform)
		{
			children.Add(child.gameObject);
		}

		pauseParent = transform.parent.GetComponent<PauseParent>();
		if (text == null) text = GetComponent<TextMeshProUGUI>();
		AddEventTrigger();
	}

	public void SetEnable(bool enable)
	{
		if (text == null) text = GetComponent<TextMeshProUGUI>();
		isActive = enable;
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
	private void AddEventTrigger()
	{
		gameObject.AddComponent<EventTrigger>();
		eventTrigger = GetComponent<EventTrigger>();
		eventTrigger.triggers = new List<EventTrigger.Entry>();

		//PointerEnter
		EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
		entryPointerEnter.eventID = EventTriggerType.PointerEnter;
		entryPointerEnter.callback.AddListener(x => { if (!isActive) text.color = selectedColor; });
		eventTrigger.triggers.Add(entryPointerEnter);

		//PointerExit
		EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
		entryPointerExit.eventID = EventTriggerType.PointerExit;
		entryPointerExit.callback.AddListener(x => { if (!isActive) text.color = unselectedColor; });
		eventTrigger.triggers.Add(entryPointerExit);

		//PointerClick
		EventTrigger.Entry entryPointerClick = new EventTrigger.Entry();
		entryPointerClick.eventID = EventTriggerType.PointerClick;
		entryPointerClick.callback.AddListener(x => { pauseParent.ActiveSetting(pauseButtonNum); });
		eventTrigger.triggers.Add(entryPointerClick);

	}

	private void SetActiveAllChildren(bool activice)
	{
		foreach (GameObject child in children)
			child.SetActive(activice);
	}
}
