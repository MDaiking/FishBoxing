using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField]
	protected int hp;
	protected int maxhp;

	PlayerDeath playerDeath;
	[SerializeField]
	private HPBar hpbar;

	private PlayerMove playerMove;

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
		if (hpbar == null)
		{
			hpbar = GameObject.FindWithTag("HPBar").GetComponent<HPBar>();
		}
		playerMove = GetComponent<PlayerMove>();
		playerDeath = GetComponent<PlayerDeath>();

	}
	virtual protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Damaged(20);
		}
	}
	virtual protected void FixedUpdate()
	{

	}

	public void Heal(int healAmount)
	{
		if (hp + healAmount >= maxhp)
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
		if (hp - damage <= 0)
		{
			hp = 0;
			if (playerDeath != null)
			{
				playerDeath.Death();
			}
		}
		else
		{
			hp -= damage;
		}
	}
}
