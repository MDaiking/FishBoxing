using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	public const float threshold = 0.01f;//0‚©0ˆÈŠO‚©‚ğ”»•Ê‚·‚é‚½‚ß‚Ì’è”BinputSystem‚Ìfloat->bool•ÏŠ·‚È‚Ç‚É

	[SerializeField]
	EquipLists equipLists;
	private void Start()
	{
		Application.targetFrameRate = 60;
		int checkEquipListsError = CheckEquipLists();
		if (checkEquipListsError != -1)
		{
			Debug.LogError("EquipLists“à‚ÌGameObjct[" + checkEquipListsError + "]‚ÍWeapon Script‚ª“ü‚Á‚Ä‚¢‚Ü‚¹‚ñ\n•Ší‚Ì‚İ“ü‚ê‚é‚±‚Æ‚ª‚Å‚«‚Ü‚·");
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
