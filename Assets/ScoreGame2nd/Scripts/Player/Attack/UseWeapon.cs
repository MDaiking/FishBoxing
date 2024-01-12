using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerEquips), typeof(PlayerInputList), typeof(Animator))]

public class UseWeapon : MonoBehaviour
{
	private PlayerInputList playerInputList;
	private Animator animator;
	private PlayerEquips playerEquips;

	private void Start()
	{
		playerEquips = GetComponent<PlayerEquips>();
		playerInputList = GetComponent<PlayerInputList>();
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		if (IsIdleAnimation() && playerInputList.IsAttackStart && playerEquips.GetNowWeapon() != null)
		{
			playerEquips.GetNowWeapon().Use();
		}
	}
	public bool IsIdleAnimation()
	{
		return (animator.GetCurrentAnimatorStateInfo(1).IsName("Idle"));
	}
}
