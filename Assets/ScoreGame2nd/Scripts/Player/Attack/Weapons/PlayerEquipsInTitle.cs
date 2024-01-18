using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipsInTitle : PlayerEquips
{
	protected override void Start()
	{
		equipWeapons.InitAviableEquips(playerEquips, nowEquip);
	}
	protected override void Update()
	{

	}
}
