using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackButton : ButtonInUI
{
	[SerializeField]
	UnityEvent afterBackAction;
	protected List<GameObject> children = new List<GameObject>();
	protected virtual void Start()
	{
		foreach (Transform child in transform.parent.transform)
		{
			children.Add(child.gameObject);
		}
	}

	protected void Back()
	{
		afterBackAction.Invoke();
		foreach (GameObject child in children)
		{
			child.SetActive(false);
		}
	}
}
