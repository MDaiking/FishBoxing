using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	none,
	melee,
}
public class Weapon : MonoBehaviour
{
	protected GameObject player;
	private UseWeapon useWeapon;
	protected bool isUsing;
	protected bool isEating;
	private bool isAttackAnimation;
	private bool canHitEnemy;
	protected Animator animator;
	protected Sprite image;
	protected int? damage = null;
	protected float? readyForSwingSpeed = null;
	protected float? swingSpeed = null;
	protected int? knockbackPower = null;
	protected float? eatSpeed = null;
	protected int? healAmount = null;

	public bool IsUsing
	{
		get{ return isUsing; }
	}
	public bool IsEating
	{
		get { return isEating; }
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
	public float ReadyForSwingSpeed
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
	public float SwingSpeed
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
	public float EatSpeed
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
	public Sprite WeaponImage
	{
		set
		{
			if (image == null)
			{
				image = value;
			}
			else
			{
				Debug.LogError("�摜�͊��ɃC���|�[�g����Ă��܂��B");
			}
		}
		get
		{
			return image;
		}
	}

	protected virtual void Start()
	{
		isUsing = false;
		animator = transform.root.GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
		useWeapon = player.GetComponent<UseWeapon>();
	}
	protected virtual void Update()
	{
		isAttackAnimation = animator.GetCurrentAnimatorStateInfo(1).IsName("Attack");
		if (!isAttackAnimation)
		{
			canHitEnemy = false;
		}
	}
	public virtual void Use()
	{
		if (!useWeapon.IsIdleAnimation())
		{
			return;
		}
		isUsing = true;
	}
	public virtual void Eat()
	{
		if(GetWeaponType() != WeaponType.melee)
		{
			return;
		}
		isEating = true;
	}
	protected virtual void DamageToEnemy(PlayerStatus enemyStatus)
	{
		if (isAttackAnimation && !canHitEnemy)
		{
			Debug.Log("damage "+ damage + " to " + enemyStatus);
			canHitEnemy = true;
			enemyStatus.Damaged((int)damage);
		}
	}
	public virtual WeaponType GetWeaponType()
	{
		return WeaponType.none;
	}
}
