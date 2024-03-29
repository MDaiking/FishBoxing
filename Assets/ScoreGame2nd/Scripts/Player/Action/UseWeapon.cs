using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerEquips), typeof(PlayerInputList), typeof(Animator))]
[RequireComponent(typeof(PlayerDeath))]

public class UseWeapon : MonoBehaviour
{
	private PlayerInputList playerInputList;
	private Animator animator;
	private PlayerEquips playerEquips;
	private CursorController cursorController;
	private PlayerDeath playerDeath;
	private bool canUse;

	private void Start()
	{
		playerEquips = GetComponent<PlayerEquips>();
		playerInputList = GetComponent<PlayerInputList>();
		animator = GetComponent<Animator>();
		playerDeath = GetComponent<PlayerDeath>();
		cursorController = GameObject.FindWithTag("GameController").GetComponent<CursorController>();
	}
	private void Update()
	{
		canUse = !cursorController.IsCursorShow;
		if (canUse && IsIdleAnimation() && !playerDeath.IsPlayerDead && playerEquips.GetNowWeapon() != null)
		{
			if (playerInputList.IsAttackStart)
			{
				playerEquips.GetNowWeapon().Use();
			}
			if (playerInputList.IsEatStart)
			{
				playerEquips.GetNowWeapon().Eat();
			}
		}
	}
	public bool IsIdleAnimation()
	{
		return (animator.GetCurrentAnimatorStateInfo(1).IsName("Idle"));
	}
}
