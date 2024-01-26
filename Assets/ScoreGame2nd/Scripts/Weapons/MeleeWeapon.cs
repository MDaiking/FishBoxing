using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshCollider), typeof(Timer))]

public class MeleeWeapon : Weapon
{
	private MeshCollider weaponCollider;

	private Timer timer;
	private PlayerStatus playerStatus;
	private PlayerInputList playerInputList;
	private float readyToAttackSpeedInAnimation;
	private float eatSpeedInAnimation;
	private CheckAttackingController checkAttacking;
	private CheckEatingController checkEating;

	public override void Setup()
	{
		base.Setup();
		weaponCollider = GetComponent<MeshCollider>();
		timer = GetComponent<Timer>();
		if (player != null)
		{
			playerInputList = player.GetComponent<PlayerInputList>();
			playerStatus = player.GetComponent<PlayerStatus>();
		}
		readyToAttackSpeedInAnimation = GetAnimationLength(animator, 1, "ReadyToAttack");
		eatSpeedInAnimation = GetAnimationLength(animator, 1, "Eating");
		checkEating = GameObject.FindWithTag("CheckEating").GetComponent<CheckEatingController>();
		checkAttacking = GameObject.FindWithTag("CheckAttacking").GetComponent<CheckAttackingController>();
	}
	protected override void Update()
	{
		base.Update();
		if (IsUsing && !canUseWeapon)
		{
			UseUpdate();
			if (playerInputList.IsAttackEnd)
			{
				FinishToUse();
			}
		}
		else if (IsEating && !canUseWeapon)
		{
			if (playerInputList.IsEat)
			{
				EatUpdate();
			}
			if (playerInputList.IsEatEnd)
			{
				FinishToEat(false);
			}
		}
	}
	public void SetWeaponCollider(bool isAviable)
	{
		weaponCollider.enabled = isAviable;
	}
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Enemy")
		{
			DamageToEnemy(collider.gameObject);
		}
	}
	public override bool Use()
	{
		if (base.Use())
		{
			SetWeaponSize((float)atAttackSize, 1.0f);
			float readySpeed = ReadyToAttackAnimationSpeed();
			animator.SetBool("IsAttack", true);
			animator.SetFloat("ReadyToAttackSpeed", readySpeed);
			animator.SetBool("CanAttack", false);
			checkAttacking.SetActive();
			timer.StartTimer((float)readyForSwingSpeed);
			return true;
		}
		else
		{
			return false;
		}
	}
	private void UseUpdate()
	{
		checkAttacking.SetAmount(timer.GetTimePercent());
		canUseWeapon = false;
	}
	private void FinishToUse()
	{

		bool canSwing = timer.StopTimer();
		animator.SetBool("IsAttack", false);
		animator.SetBool("CanAttack", canSwing);
		checkAttacking.SetInactive();
		isUsing = false;
		if (!canSwing)
		{
			SetDefaultWeaponSize(0.2f);
		}
	}
	public override void Eat()
	{
		base.Eat();
		timer.StartTimer((float)eatSpeed);
		float _eatingSpeed = EatAnimationSpeed();
		animator.SetFloat("EatingSpeed", _eatingSpeed);
		animator.SetBool("IsEating", true);
	}
	private void EatUpdate()
	{
		float eatper = timer.GetTimePercent();
		if (eatper >= 1.0f)
		{
			FinishToEat(true);
		}
		else
		{
			checkEating.SetFishMaskPercent(eatper);
		}
	}
	private void FinishToEat(bool isFinished)
	{
		timer.StopTimer();
		animator.SetBool("IsEating", false);
		if (isFinished)
		{
			playerStatus.Heal((int)healAmount);
		}
		else
		{
			checkEating.SetFishMaskPercent(0.0f);

		}
		isEating = false;
	}

	private float ReadyToAttackAnimationSpeed()
	{
		if (readyToAttackSpeedInAnimation != -1.0f && readyForSwingSpeed != 0.0f)
		{
			return (float)readyToAttackSpeedInAnimation / (float)readyForSwingSpeed;
		}
		else
		{
			return 1.0f;
		}
	}
	private float EatAnimationSpeed()
	{
		if (eatSpeedInAnimation != -1.0f && eatSpeed != 0.0f)
		{
			return (float)eatSpeedInAnimation / (float)eatSpeed;
		}
		else
		{
			return 1.0f;
		}
	}
	private float GetAnimationLength(Animator animator, int layer, string clipName)
	{
		var runtimeController = animator.runtimeAnimatorController;
		var controller = runtimeController as UnityEditor.Animations.AnimatorController;

		var stateMachine = controller.layers[layer].stateMachine;
		foreach (var state in stateMachine.states)
		{
			if (state.state.name == clipName)
			{
				AnimationClip clip = state.state.motion as AnimationClip;
				return clip.length;
			}
		}
		Debug.LogError("The " + clipName + "is not found in layer " + layer + ".");
		return -1.0f;
	}
	public override WeaponType GetWeaponType()
	{
		return WeaponType.melee;
	}
}
