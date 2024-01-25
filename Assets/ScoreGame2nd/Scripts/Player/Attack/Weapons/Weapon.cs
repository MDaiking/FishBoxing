using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum WeaponType
{
	none,
	melee,
}
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
	protected GameObject player;
	protected UseWeapon useWeapon;
	private PlayerMove playerMove;
	protected bool isUsing;
	protected bool isEating;
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
	protected float? defaultSize = null;
	protected float? atAttackSize = null;
	private Transform rootTransform;
	protected bool isDefaultSize;
	protected bool isBackingToDefaultSize;
	protected Tween changeSizeTween;

	protected AudioSource audioSource;
	protected AudioClip swingSound;
	protected AudioClip hitSound;
	protected AudioClip eatSound;

	public virtual void Setup()
	{
		isUsing = false;
		isDefaultSize = false;
		isBackingToDefaultSize = false;
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
		if (!isDefaultSize && useWeapon.IsIdleAnimation())
		{
			SetDefaultWeaponSize(0.2f);
		}
	}
	public virtual void Use()
	{
		if (!GetCanActivateWeapon())
		{
			return;
		}
		isUsing = true;
	}
	public virtual void Eat()
	{
		if (GetWeaponType() != WeaponType.melee)
		{
			return;
		}
		isEating = true;
	}
	protected virtual void DamageToEnemy(GameObject enemy)
	{
		if (isAttackAnimation && !canHitEnemy)
		{
			Debug.Log("damage " + damage + " to " + enemy + "(knockback: " + knockbackPower + ", " + knockbackResistance + ")");
			canHitEnemy = true;
			PlayerStatus enemyStatus = enemy.GetComponent<PlayerStatus>();
			if (enemyStatus != null)
			{
				enemyStatus.Damaged((int)damage);
			}
			else
			{
				Debug.LogError(enemyStatus + "に\"PlayerStatus\"がコンポーネントされていません");
			}
			PlayerKnockback enemyKB = enemy.GetComponent<PlayerKnockback>();
			if (enemyKB != null)
			{
				enemyKB.SetKnockback(player.transform.rotation.eulerAngles, (float)knockbackPower, (float)knockbackResistance);
			}
			else
			{
				Debug.LogError(enemyStatus + "に\"PlayerKnockback\"がコンポーネントされていません");
			}
		}
	}
	protected void SetWeaponSize(float size, float setTime)
	{
		changeSizeTween = rootTransform.DOScale(new Vector3(size, size, size), setTime)
			.OnStart(() => { if (size == defaultSize) { isBackingToDefaultSize = true; } })
			.OnComplete(() =>
			{
				isDefaultSize = (size == defaultSize);
				isBackingToDefaultSize = false;
			});
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
			isDefaultSize = true;
			isBackingToDefaultSize = false;

		}
		else
		{
			float _defaultSize = (float)defaultSize;
			changeSizeTween = rootTransform.DOScale(new Vector3(_defaultSize, _defaultSize, _defaultSize), setTime)
				.OnStart(() => { isBackingToDefaultSize = true; })
				.OnComplete(() =>
				{
					isDefaultSize = true;
					isBackingToDefaultSize = false;
				});
		}
	}
	public bool GetCanActivateWeapon()
	{
		return (useWeapon.IsIdleAnimation() && !isBackingToDefaultSize);
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
				Debug.LogError("ダメージ量を再定義することはできません");
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
				Debug.LogError("構え速度を再定義することはできません");
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
				Debug.LogError("振り速度を再定義することはできません");
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
				Debug.LogError("ノックバック力を再定義することはできません");
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
				Debug.LogError("ノックバック抵抗力を再定義することはできません");
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
				Debug.LogError("食事速度を再定義することはできません");
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
				Debug.LogError("回復量を再定義することはできません");
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
				Debug.LogError("デフォルトのサイズを再定義することはできません");
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
				Debug.LogError("攻撃時のサイズを再定義することはできません");
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
				Debug.LogError("画像は既にインポートされています。");
			}
		}
		get
		{
			return image;
		}
	}
}
