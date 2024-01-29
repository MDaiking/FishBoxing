using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
	[SerializeField]
	private TitleMenuController titleMenuController;
    private List<GameObject> children = new List<GameObject>();
	private void Start()
	{
		foreach(Transform child in transform)
		{
			children.Add(child.gameObject);
		}
		ToggleStartMenuActive(false);
	}
	public void ToggleStartMenuActive(bool active)
	{
		foreach (GameObject child in children)
		{
			child.SetActive(active);
		}
	}

}
