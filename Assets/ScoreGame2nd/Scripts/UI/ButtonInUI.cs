using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInUI : MonoBehaviour
{
	private EventTrigger eventTrigger;
	protected bool isSelected;

	protected void AddEventTrigger(Action enterAction, Action exitAction, Action clickAction)
	{
		gameObject.AddComponent<EventTrigger>();
		eventTrigger = GetComponent<EventTrigger>();
		eventTrigger.triggers = new List<EventTrigger.Entry>();

		//PointerEnter
		EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
		entryPointerEnter.eventID = EventTriggerType.PointerEnter;
		entryPointerEnter.callback.AddListener(x => { enterAction(); });
		eventTrigger.triggers.Add(entryPointerEnter);

		//PointerExit
		EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
		entryPointerExit.eventID = EventTriggerType.PointerExit;
		entryPointerExit.callback.AddListener(x => { exitAction(); });
		eventTrigger.triggers.Add(entryPointerExit);

		//PointerClick
		EventTrigger.Entry entryPointerClick = new EventTrigger.Entry();
		entryPointerClick.eventID = EventTriggerType.PointerClick;
		entryPointerClick.callback.AddListener(x => { clickAction(); });
		eventTrigger.triggers.Add(entryPointerClick);

	}
}
