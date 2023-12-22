using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyStatus : PlayerStatus
{
    private int regenStartSec = 2;//�_���[�W���󂯂Ă��玩���񕜂��N������܂ł̎���
    private int regenCount;
    private int regenAmount = 5;//1 fixedupdate�Ŏ󂯂鎩���񕜂̗�
    override protected void Start()
    {
        maxhp = hp;
        regenCount = 0;
    }

    override protected void Update()
    {
        base.Update();
    }
    override protected void FixedUpdate()
    {
        Regeneration();
    }
	public override void Damaged(int damage)
	{
		base.Damaged(damage);
        regenCount = 0;
	}

    private void Regeneration()
	{
        if(regenCount >= regenStartSec * 60)
		{
            Heal(regenAmount);
		}
		else
		{
            ++regenCount;
		}
	}
}
