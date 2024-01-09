using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	protected bool isUsing;
	private bool canAttack;
	private bool canHitEnemy;
	protected Animator animator;
	private int? damage = null;
	private int? readyForSwingSpeed = null;
	private int? swingSpeed = null;
	private int? knockbackPower = null;
	private int? eatSpeed = null;
	private int? healAmount = null;

	public bool IsUsing
	{
		get{ return isUsing; }
	}
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
	public int ReadyForSwingSpeed
	{
		set
		{
			if (readyForSwingSpeed == null)
			{
				readyForSwingSpeed = value;
			}
			else
			{
				Debug.LogError("�\�����x���Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}
	public int SwingSpeed
	{
		set
		{
			if (swingSpeed == null)
			{
				swingSpeed = value;
			}
			else
			{
				Debug.LogError("�U�葬�x���Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}
	public int KnockbackPower
	{
		set
		{
			if (knockbackPower == null)
			{
				knockbackPower = value;
			}
			else
			{
				Debug.LogError("�m�b�N�o�b�N�͂��Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}
	public int EatSpeed
	{
		set
		{
			if (eatSpeed == null)
			{
				eatSpeed = value;
			}
			else
			{
				Debug.LogError("�H�����x���Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}
	public int HealAmount
	{
		set
		{
			if (healAmount == null)
			{
				healAmount = value;
			}
			else
			{
				Debug.LogError("�񕜗ʂ��Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}

	protected virtual void Start()
	{
		isUsing = false;
		animator = transform.root.GetComponent<Animator>();
	}
	protected virtual void Update()
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
			Debug.Log("damage "+ damage + " to " + enemyStatus);
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
