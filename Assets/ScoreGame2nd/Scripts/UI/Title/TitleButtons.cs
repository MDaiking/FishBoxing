using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();

	private void Start()
	{
		foreach(Transform child in transform)
		{
			children.Add(child.gameObject);
		}
	}
	public void ToggleAllButtonsActive(bool active)
	{
		foreach(GameObject child in children)
		{
			child.SetActive(active);
		}
	}
}
