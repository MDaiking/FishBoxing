using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();

	private void Start()
	{
		foreach(Transform child in transform)
		{
			children.Add(child.gameObject);
		}
	}
	public void ToggleCreditActive(bool active)
	{
		foreach(GameObject child in children)
		{
			child.SetActive(active);
		}
	}
}
