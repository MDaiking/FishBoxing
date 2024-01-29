using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Net;
using UnityEngine.SceneManagement;
public enum ServerTypeEnum
{
	host, client,
}
public class GameManager : MonoBehaviour
{
	public const float threshold = 0.01f;//0‚©0ˆÈŠO‚©‚ğ”»•Ê‚·‚é‚½‚ß‚Ì’è”BinputSystem‚Ìfloat->bool•ÏŠ·‚È‚Ç‚É
	string hostname = Dns.GetHostName();

	[SerializeField]
	EquipLists equipLists;
	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
	private void Start()
	{
		Application.targetFrameRate = 60;
		int checkEquipListsError = CheckEquipLists();
		if (checkEquipListsError != -1)
		{
			Debug.LogError("EquipLists“à‚ÌGameObjct[" + checkEquipListsError + "]‚ÍWeapon Script‚ª“ü‚Á‚Ä‚¢‚Ü‚¹‚ñ\n•Ší‚Ì‚İ“ü‚ê‚é‚±‚Æ‚ª‚Å‚«‚Ü‚·");
		}
		GetMyIPAddress();
	}
	public string GetMyIPAddress()
	{
		IPAddress[] adrList = Dns.GetHostAddresses(hostname);
		return adrList[1].ToString();
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
