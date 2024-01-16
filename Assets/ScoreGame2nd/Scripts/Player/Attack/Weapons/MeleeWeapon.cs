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
	private float readySpeedInAnimation;
	private CheckAttackingController checkAttacking;
	private CheckEatingController checkEating;
	protected override void Start()
	{
		base.Start();
		weaponCollider = GetComponent<MeshCollider>();
		timer = GetComponent<Timer>();
		playerInputList = player.GetComponent<PlayerInputList>();
		readySpeedInAnimation = GetAnimationLength(animator, 1, "ReadyToAttack");
		checkEating = GameObject.FindWithTag("CheckEating").GetComponent<CheckEatingController>();
		checkAttacking = GameObject.FindWithTag("CheckAttacking").GetComponent<CheckAttackingController>();
		playerStatus = player.GetComponent<PlayerStatus>();
	}
	protected override void Update()
	{
		base.Update();
		if (IsUsing)
		{
			UseUpdate();
			if (playerInputList.IsAttackEnd)
			{
				FinishToUse();
			}
		}
		else if (IsEating)
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
			DamageToEnemy(collider.GetComponent<PlayerStatus>());
		}
	}
	public override void Use()
	{
		base.Use();
		float readySpeed = ControlToReadyAnimationSpeed();
		animator.SetFloat("ReadySpeed", readySpeed);
		animator.SetBool("IsAttack", true);
		animator.SetBool("CanAttack", false);
		checkAttacking.SetActive();
		timer.StartTimer((float)readyForSwingSpeed);
	}
	private void UseUpdate()
	{
		checkAttacking.SetAmount(timer.GetTimePercent());
	}
	private void FinishToUse()
	{
		bool canSwing = timer.StopTimer();
		animator.SetBool("IsAttack", false);
		animator.SetBool("CanAttack", canSwing);
		checkAttacking.SetInactive();
		isUsing = false;
	}
	public override void Eat()
	{
		base.Eat();
		timer.StartTimer((float)eatSpeed);
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
	private float ControlToReadyAnimationSpeed()
	{
		if (readySpeedInAnimation != -1.0f && readyForSwingSpeed != 0.0f)
		{
			return (float)readySpeedInAnimation / (float)readyForSwingSpeed;
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
