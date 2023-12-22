using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerEquips))]
[RequireComponent(typeof(PlayerInputList))]
public class UseWeapon : MonoBehaviour
{
	private PlayerInputList playerInputList;

	private PlayerEquips playerEquips;

	private void Start()
	{
		playerEquips = GetComponent<PlayerEquips>();
		playerInputList = GetComponent<PlayerInputList>();
	}
	private void Update()
	{
		if (playerInputList.StartToGetAttack && playerEquips.GetNowWeapon() != null)
		{
			playerEquips.GetNowWeapon().Use();
		}

	}
}
