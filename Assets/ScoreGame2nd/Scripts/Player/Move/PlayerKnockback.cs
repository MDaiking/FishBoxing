using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerKnockback : Unity.Netcode.NetworkBehaviour
{
	private bool isKnockback;
	private bool isCrouched;
	public bool IsCrouched
	{
		set { isCrouched = value; }
	}
	[SerializeField]
	private float crouchResistance = 2.0f;
	private int knockbackTimer;
	private Vector3 knockbackDirection;
	private float knockbackPower;
	private float knockbackResistance;

	private void Start()
	{
		if (IsOwner)
		{
			isKnockback = false;
			isCrouched = false;
		}
	}
	private void FixedUpdate()
	{
		if (IsOwner && isKnockback)
		{
			++knockbackTimer;
		}
	}
	public void SetKnockback(Vector3 direction, float power, float resistance)
	{
		direction.y = 0.0f;
		knockbackDirection = direction.normalized;
		knockbackPower = power;
		if (isCrouched)
		{
			knockbackPower /= crouchResistance;
			knockbackDirection /= crouchResistance;
		}

		knockbackResistance = resistance;
		isKnockback = true;
		knockbackTimer = 0;
	}
	public Vector3 CalcKnockback()
	{
		if (isKnockback)
		{
			float magnitude = -(float)(knockbackTimer * knockbackTimer) / knockbackResistance + knockbackPower;
			if (magnitude > 0.0f)
			{
				return knockbackDirection * magnitude;
			}
			else
			{
				isKnockback = false;
			}
		}
		return Vector3.zero;
	}
}
