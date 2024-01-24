using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UseWeapon), typeof(PlayerInputList))]

public class PlayerEquips : MonoBehaviour
{
	[SerializeField]
	protected int maxEquip = 3;
	public int MaxEquip
	{
		get { return maxEquip; }
	}
	protected int nowEquip = 0;
	public int NowEquip
	{
		get { return nowEquip; }
	}
	[SerializeField]
	protected List<int> playerEquips = new List<int>();
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
		if (playerInputList == null)
		{
			Debug.LogError("PlayerInputList���R���|�[�l���g����Ă��܂���");
		}
		checkEating = GameObject.FindWithTag("CheckEating").GetComponent<CheckEatingController>();
		playerWeapons = equipWeapons.InitAviableEquips(playerEquips, nowEquip);
	}
	protected virtual void Update()
	{
		ScrollWeapon();
	}
	private void ScrollWeapon()//�z�C�[�������ǂݎ���ĕ�����X�N���[���ŕύX������
	{
		if (!GetNowWeapon().GetCanActivateWeapon())
		{
			return;
		}
		int scrollCount = -(int)playerInputList.SwitchWeaponAxis / 120;
		if (scrollCount != 0)
		{
			AddNowEquipNum(scrollCount);
			equipWeapons.ChangeWeapon(playerEquips[nowEquip]);
			checkEating.SetEatingFishSprite(GetNowEquipParam());
			GetNowWeapon().SetDefaultWeaponSize(0.0f);
		}
	}
	private void AddNowEquipNum(int addnum)//addnum����������炷�B�}�C�i�X�ɂ��Ή�
	{
		int n = nowEquip + addnum;
		if (0 > n) nowEquip = (maxEquip + n % maxEquip);
		else if (n >= nowEquip) nowEquip = (n % maxEquip);
		else nowEquip = n;
	}

	public EquipParam GetNowEquipParam()
	{
		return GetEquipParam(NowEquip);
	}
	public EquipParam GetEquipParam(int equipNum)
	{
		return equipLists.equipParamList[equipNum];
	}
	public Weapon GetNowWeapon()
	{
		return playerWeapons[nowEquip];
	}
}
