using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public const float threshold = 0.01f;//0か0以外かを判別するための定数。inputSystemのfloat->bool変換などに

	[SerializeField]
	private float distance;
	[SerializeField]
	EquipLists equipLists;
	private void Start()
	{
		Application.targetFrameRate = 60;
		int checkEquipListsError = CheckEquipLists();
		if (checkEquipListsError != -1)
		{
			Debug.LogError("EquipLists内のGameObjct[" + checkEquipListsError + "]はWeapon Scriptが入っていません\n武器のみ入れることができます");
		}
	}
	private int CheckEquipLists()
	{
		int i = 0;
		foreach (EquipParam equipParam in equipLists.equipParamList)
		{
			if (!equipParam.equipObject.GetComponent<Weapon>())
			{
				return i;
			}
			++i;
		}
		return -1;
	}
}
