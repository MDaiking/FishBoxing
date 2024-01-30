using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UseWeapon), typeof(PlayerInputList))]

public class PlayerEquips : MonoBehaviour
{
	[SerializeField]
	protected int maxEquip = 1;
	public int MaxEquip
	{
		get { return maxEquip; }
	}
	[SerializeField]
	protected int nowEquip = 0;
	public int NowEquip
	{
		get { return nowEquip; }
	}
	[SerializeField]
	protected List<int> playerEquips = new List<int>();
	[SerializeField]
	protected List<Weapon> playerWeapons = new List<Weapon>();
	public List<Weapon> PlayerWeapons
	{
		get { return playerWeapons; }
	}

	[SerializeField]
	protected EquipWeapons equipWeapons;
	[SerializeField]
	protected EquipLists equipLists;
	private PlayerInputList playerInputList;
	private UseWeapon useWeapon;

	private CheckEatingController checkEating;

	protected virtual void Start()
	{
		playerInputList = GetComponent<PlayerInputList>();
		useWeapon = GetComponent<UseWeapon>();
		GameObject.FindWithTag("SelectWeapons").GetComponent<SelectWeaponParent>().PlayerEquip = this;
		checkEating = GameObject.FindWithTag("CheckEating").GetComponent<CheckEatingController>();
		playerWeapons = equipWeapons.InitAviableEquips(playerEquips, nowEquip);
	}
	protected virtual void Update()
	{
		ScrollWeapon();
	}
	private void ScrollWeapon()//ホイール操作を読み取って武器をスクロールで変更させる
	{
		if (GetNowWeapon() == null || !GetNowWeapon().CanUseWeapon)
		{
			return;
		}
		int scrollCount = -(int)playerInputList.SwitchWeaponAxis / 120;
		if (scrollCount != 0)
		{
			AddNowEquipNum(scrollCount);
			equipWeapons.ChangeWeapon(nowEquip);
			checkEating.SetEatingFishSprite(GetNowEquipParam());
			GetNowWeapon().SetDefaultWeaponSize(0.0f);
		}
	}
	private void AddNowEquipNum(int addnum)//addnum分武器をずらす。マイナスにも対応
	{
		int havingEquips = playerWeapons.Count;
		if (havingEquips == 1)
		{
			return;
		}
		int n = nowEquip + addnum;
		if (0 > n) nowEquip = (havingEquips + n % havingEquips);
		else if (n >= nowEquip) nowEquip = (n % havingEquips);
		else nowEquip = n;
	}

	public EquipParam GetNowEquipParam()
	{
		return GetEquipParam(NowEquip);
	}
	public void ResetPlayerEquip(int equipNum)
	{
		List<int> equipNums = new List<int>();
		equipNums.Add(equipNum);
		ResetPlayerEquip(equipNums);

	}
	public void ResetPlayerEquip(List<int> equipNums)
	{
		foreach (Weapon weapon in playerWeapons)
		{
			Destroy(weapon.gameObject);
		}
		nowEquip = 0;
		playerEquips.Clear();
		playerEquips = equipNums;
		playerWeapons.Clear();
		playerWeapons = equipWeapons.InitAviableEquips(playerEquips, nowEquip);
	}
	public EquipParam GetEquipParam(int equipNum)
	{
		return equipLists.equipParamList[equipNum];
	}
	public Weapon GetNowWeapon()
	{
		if (playerWeapons.Count == 0)
		{
			return null;
		}
		else
		{
			return playerWeapons[nowEquip];
		}
	}
}
