using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	List<GameObject> children = new List<GameObject>();
	[SerializeField,Tooltip("�n�߂ɕ\������郁�j���[��")]
	private bool showInStartMenu = false;
	private void Start()
	{
		foreach (Transform child in transform)
		{
			children.Add(child.gameObject);
		}
		ToggleMenuActive(showInStartMenu);
	}
	public void ToggleMenuActive(bool active)
	{
		foreach (GameObject child in children)
		{
			child.SetActive(active);
		}
	}
}
