using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerInvisible : MonoBehaviour
{
	[SerializeField]
	private Renderer[] playerRenderer;
	[SerializeField]
	private bool isInvisible = false;
	private bool isAlreadyGetRenderer = false;

	private void Start()
	{
		GetAllRenderer();
	}
	private void GetAllRenderer()
	{
		if (!isAlreadyGetRenderer)
		{
			isAlreadyGetRenderer = true;
			playerRenderer = GetComponentsInChildren<Renderer>();
		}

	}
	public void ToggleInvisible()
	{
		if (!isInvisible)
		{
			isInvisible = true;
			EnableInvisible();
		}
		else
		{
			isInvisible = false;
			DisableInvisible();
		}
	}
	public void SetInvisible(bool flag)
	{
		isInvisible = flag;
		if (isInvisible)
		{
			EnableInvisible();
		}
		else
		{
			DisableInvisible();
		}
	}
	private void EnableInvisible()
	{
		GetAllRenderer();

		foreach (Renderer r in playerRenderer)
		{
			r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}
	}
	private void DisableInvisible()
	{
		GetAllRenderer();

		foreach (Renderer r in playerRenderer)
		{
			r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		}
	}
}
