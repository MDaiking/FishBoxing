using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshCollider))]

public class MeleeWeapon : Weapon
{
	private MeshCollider weaponCollider;
	protected override void Start()
	{
		base.Start();
		weaponCollider = GetComponent<MeshCollider>();
	}
	protected override void Update()
	{
		base.Update();
		if (IsUsing)
		{

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
		animator.SetBool("IsAttack", true);
	}
}
