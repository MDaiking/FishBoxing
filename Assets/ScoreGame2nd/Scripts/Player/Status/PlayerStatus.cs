using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField]
	protected int hp;
	protected int maxhp;

	[SerializeField]
	private HPBar hpbar;

	public int HP
	{
		get { return hp; }
		set
		{
			if (value <= maxhp) hp = value;
			hpbar.ChangeHPBar(hp, maxhp);
		}
	}
	public float GetHPpc()
	{
		return (float)hp / maxhp;
	}


	virtual protected void Start()
	{
		maxhp = hp;
		if (hpbar == null) hpbar = GameObject.FindWithTag("HPBar").GetComponent<HPBar>();
	}
	virtual protected void Update()
	{

	}
	virtual protected void FixedUpdate()
	{

	}

	public void Heal(int healAmount)
	{
		if(hp + healAmount >= maxhp)
		{
			hp = maxhp;
		}
		else
		{
			hp += healAmount;
		}
	}
	virtual public void Damaged(int damage)
	{
		if(hp - damage <= 0)
		{
			hp = 0;
			Death();
		}
		else
		{
			hp -= damage;
		}
	}
	private void Death()
	{

	}
}
