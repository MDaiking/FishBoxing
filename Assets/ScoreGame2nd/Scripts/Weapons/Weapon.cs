using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum WeaponType
{
	none,
	melee,
}
[RequireComponent(typeof(AudioSource),typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
	protected GameObject player;
	protected UseWeapon useWeapon;
	private PlayerMove playerMove;
	[SerializeField]
	protected bool isUsing;
	[SerializeField]
	protected bool isEating;
	[SerializeField]
	protected bool isAfterEatingCooldown;
	private bool isAttackAnimation;
	private bool canHitEnemy;
	protected Animator animator;
	protected Sprite image;

	protected int? damage = null;
	protected float? readyForSwingSpeed = null;
	protected float? swingSpeed = null;
	protected float? knockbackPower = null;
	protected float? knockbackResistance = null;
	protected float? eatSpeed = null;
	protected int? healAmount = null;
	protected float? coolTime = null;
	protected float? defaultSize = null;
	protected float? atAttackSize = null;

	private Transform rootTransform;
	[SerializeField]
	protected bool canUseWeapon;
	public bool CanUseWeapon
	{
		get { return canUseWeapon; }
	}
	protected Tween changeSizeTween;

	protected AudioSource audioSource;
	protected AudioClip swingSound;
	protected AudioClip hitSound;
	protected AudioClip eatSound;

	public virtual void Setup()
	{
		isUsing = false;
		canUseWeapon = true;
		isAfterEatingCooldown = false;
		animator = transform.root.GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
		playerMove = player.GetComponent<PlayerMove>();
		rootTransform = transform.parent;
		audioSource = GetComponent<AudioSource>();
		if (player != null)
		{
			useWeapon = player.GetComponent<UseWeapon>();
		}
	}
	protected virtual void Update()
	{
		isAttackAnimation = animator.GetCurrentAnimatorStateInfo(1).IsName("Attack");
		if (!isAttackAnimation)
		{
			canHitEnemy = false;
		}
		if (!canUseWeapon && !IsUsing && !IsEating && useWeapon.IsIdleAnimation() && !isAfterEatingCooldown)
		{
			canUseWeapon = true;
			SetDefaultWeaponSize(0.2f);
		}
	}
	public virtual bool Use()
	{
		if (!canUseWeapon || IsUsing || isAfterEatingCooldown)
		{
			return false;
		}
		isUsing = true;
		canUseWeapon = false;
		audioSource.PlayOneShot(swingSound);
		return true;
	}
	public virtual bool Eat()
	{
		if (GetWeaponType() != WeaponType.melee || !canUseWeapon || IsEating || isAfterEatingCooldown)
		{
			return false;
		}
		isEating = true;
		canUseWeapon = false;
		return true;
	}
	protected virtual void DamageToEnemy(GameObject enemy)
	{
		if (isAttackAnimation && !canHitEnemy)
		{
			//Debug.Log("damage " + damage + " to " + enemy + "\n(knockback: " + knockbackPower + ", " + knockbackResistance + ")");
			canHitEnemy = true;
			PlayerStatus enemyStatus = enemy.GetComponent<PlayerStatus>();
			if (enemyStatus != null)
			{
				audioSource.PlayOneShot(hitSound);
				enemyStatus.Damaged((int)damage);
			}
			else
			{
				Debug.LogError($"{enemyStatus}��\"PlayerStatus\"���R���|�[�l���g����Ă��܂���");
			}
			PlayerKnockback enemyKB = enemy.GetComponent<PlayerKnockback>();
			if (enemyKB != null)
			{
				enemyKB.SetKnockback(player.transform.forward, (float)knockbackPower, (float)knockbackResistance);
			}
			else
			{
				Debug.LogError($"{enemyStatus}��\"PlayerKnockback\"���R���|�[�l���g����Ă��܂���");
			}
		}
	}
	protected void SetWeaponSize(float size, float setTime)
	{
		changeSizeTween.Kill(false);
		changeSizeTween = rootTransform.DOScale(new Vector3(size, size, size), setTime);
	}
	protected void SetWeaponSize(float? size, float setTime)
	{
		SetWeaponSize((float)size, setTime);
	}
	public void SetDefaultWeaponSize(float setTime)
	{
		if (setTime == 0.0f)
		{
			float _defaultSize = (float)defaultSize;
			rootTransform.localScale = new Vector3(_defaultSize, _defaultSize, _defaultSize);
		}
		else
		{
			float _defaultSize = (float)defaultSize;
			changeSizeTween.Kill(false);
			changeSizeTween = rootTransform.DOScale(new Vector3(_defaultSize, _defaultSize, _defaultSize), setTime)
				.OnComplete(() =>
				{
					canUseWeapon = true;
				});
		}
	}
	public virtual WeaponType GetWeaponType()
	{
		return WeaponType.none;
	}
	public bool IsUsing
	{
		get { return isUsing; }
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
	public float KnockbackPower
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
	public float KnockbackResistance
	{
		set
		{
			if (knockbackResistance == null)
			{
				knockbackResistance = value;
			}
			else
			{
				Debug.LogError("�m�b�N�o�b�N��R�͂��Ē�`���邱�Ƃ͂ł��܂���");
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
	public float CoolTime
	{
		set
		{
			if (coolTime == null)
			{
				coolTime = value;
			}
			else
			{
				Debug.LogError("�N�[���^�C�����Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}
	public float DefaultSize
	{
		set
		{
			if (defaultSize == null)
			{
				defaultSize = value;
			}
			else
			{
				Debug.LogError("�f�t�H���g�̃T�C�Y���Ē�`���邱�Ƃ͂ł��܂���");
			}
		}
	}
	public float AtAttackSize
	{
		set
		{
			if (atAttackSize == null)
			{
				atAttackSize = value;
			}
			else
			{
				Debug.LogError("�U�����̃T�C�Y���Ē�`���邱�Ƃ͂ł��܂���");
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
	public AudioClip SwingSound
	{
		set
		{
			if (swingSound == null)
			{
				swingSound = value;
			}
			else
			{
				Debug.LogError("\"swingSound\"�����͊��ɓo�^����Ă��܂��B");
			}
		}
		get
		{
			return swingSound;
		}
	}
	public AudioClip HitSound
	{
		set
		{
			if (hitSound == null)
			{
				hitSound = value;
			}
			else
			{
				Debug.LogError("\"hitSound\"�����͊��ɓo�^����Ă��܂��B");
			}
		}
		get
		{
			return hitSound;
		}
	}
	public AudioClip EatSound
	{
		set
		{
			if (eatSound == null)
			{
				eatSound = value;
			}
			else
			{
				Debug.LogError("\"eatSound\"�����͊��ɓo�^����Ă��܂��B");
			}
		}
		get
		{
			return eatSound;
		}
	}
}
