using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapons : MonoBehaviour
{
	[SerializeField]
	private EquipLists equipLists;

	private Quaternion defaultEquipRotation = new Quaternion(-0.683012724f, 0.183012694f, -0.183012694f, 0.683012724f);
	public List<Weapon> InitAviableEquips(List<int> nowEquipList, int startEquip)
	{
		List<Weapon> weapons = new List<Weapon>();
		int i = 0;
		foreach (int nowEquipNum in nowEquipList)
		{
			EquipParam equipParam = equipLists.equipParamList[nowEquipNum];
			GameObject equip = Instantiate(equipParam.equipObject, transform);
			equip.transform.localPosition = equipParam.offset;
			equip.transform.localRotation = defaultEquipRotation;
			Weapon weapon = equip.GetComponent<Weapon>();

			weapon.Damage = equipParam.damage;
			weapon.ReadyForSwingSpeed = equipParam.readyForSwingSpeed;
			weapon.SwingSpeed = equipParam.swingSpeed;
			weapon.KnockbackPower = equipParam.knockbackPower;
			weapon.KnockbackResistance = equipParam.knockbackResistance;
			weapon.EatSpeed = equipParam.eatSpeed;
			weapon.HealAmount = equipParam.healAmount;
			weapon.WeaponImage = equipParam.equipImage;
			weapon.DefaultSize = equipParam.defaultSize;
			weapon.AtAttackSize = equipParam.atAttackSize;
			weapon.Setup();
			weapons.Add(weapon);

			equip.SetActive(i++ == startEquip);
		}
		return weapons;
	}
	public void ChangeWeapon(int equipNum)
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			transform.GetChild(i).gameObject.SetActive(i == equipNum);
		}
	}
}
