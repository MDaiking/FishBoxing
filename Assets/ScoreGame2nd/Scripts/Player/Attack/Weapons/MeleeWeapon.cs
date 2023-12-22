using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	private MeshCollider weaponCollider;
	protected override void Start()
	{
		base.Start();
		weaponCollider = GetComponent<MeshCollider>();
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
		animator.SetBool("CanMeleeAttack", true);
	}
}
