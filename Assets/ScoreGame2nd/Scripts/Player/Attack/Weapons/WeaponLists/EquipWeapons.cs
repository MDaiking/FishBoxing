using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapons : MonoBehaviour
{
	[SerializeField]
	private EquipLists equipLists;

	private Quaternion defaultEquipRotation = new Quaternion(-0.707106769f, 0, 0, 0.707106769f);
	public List<Weapon> InitAviableEquips(List<int> nowEquipList, int startEquip)
	{
		List<Weapon> weapons = new List<Weapon>();
		foreach (int nowEquipNum in nowEquipList)
		{
			EquipParam equipParam = equipLists.equipParamList[nowEquipNum];
			GameObject equip = Instantiate(equipParam.equipObject, transform);
			equip.transform.localPosition = equipParam.offset;
			equip.transform.localRotation = defaultEquipRotation;
			weapons.Add(equip.GetComponent<Weapon>());

		}
		ChangeWeapon(startEquip);
		return weapons;
	}
	public void ChangeWeapon(int equipNum)
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			if (i == equipNum) transform.GetChild(i).gameObject.SetActive(true);
			else transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}