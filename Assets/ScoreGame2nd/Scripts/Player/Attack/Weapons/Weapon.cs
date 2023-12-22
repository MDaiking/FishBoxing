using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private bool canAttack;
	private bool canHitEnemy;
	protected Animator animator;
	private int? damage = null;
	public int Damage
	{
		set
		{
			if (damage == null)
			{
				damage = value;
			}
			else
			{
				Debug.LogError("�_���[�W�ʂ��Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}

	protected virtual void Start()
	{
		animator = transform.root.GetComponent<Animator>();
	}
	private void Update()
	{
		canAttack = animator.GetCurrentAnimatorStateInfo(1).IsName("Attack");
		if (!canAttack)
		{
			canHitEnemy = false;
		}
	}
	public virtual void Use()
	{
		if (!IsIdleAnimation)
		{
			return;
		}
	}
	protected virtual void DamageToEnemy(PlayerStatus enemyStatus)
	{
		if (canAttack && !canHitEnemy)
		{
			canHitEnemy = true;
			enemyStatus.Damaged((int)damage);
		}
	}
	//UpperBody���C���[��Idle�A�j���[�V��������true��Ԃ�
	protected bool IsIdleAnimation
	{
		get { return (animator.GetCurrentAnimatorStateInfo(1).IsName("Idle")); }
	}
}
