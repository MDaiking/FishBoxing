using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInputList))]

public class PlayerEquips : MonoBehaviour
{
    [SerializeField]
    private int maxEquip = 3;
    public int MaxEquip
	{
        get{ return maxEquip; }
	}
    private int nowEquip = 0;
    public int NowEquip
    {
        get { return nowEquip; }
    }
    [SerializeField]
    private List<int> playerEquips = new List<int>();
    private List<Weapon> playerWeapons = new List<Weapon>();
    public List<Weapon> PlayerWeapons
	{
        get{ return playerWeapons; }
	}
    [SerializeField]
    private PlayerStatus playerStatus;
    [SerializeField]
    private EquipWeapons equipWeapons;
    [SerializeField]
    private EquipLists equipLists;
    private CheckEatingController checkEating;

    private PlayerInputList pil;

    private void Start()
    {
        pil = GetComponent<PlayerInputList>();
        checkEating = GameObject.FindWithTag("CheckEating").GetComponent<CheckEatingController>();
        playerWeapons = equipWeapons.InitAviableEquips(playerEquips,nowEquip);
    }
    private void Update()
    {
        ScrollWeapon();
    }
    private void ScrollWeapon()//ホイール操作を読み取って武器をスクロールで変更させる
	{
        int scrollCount = -(int)pil.SwitchWeaponAxis / 120;
        if(scrollCount != 0)
		{
            AddNowEquipNum(scrollCount);
            equipWeapons.ChangeWeapon(playerEquips[nowEquip]);
            checkEating.SetEatingFishSprite(GetNowEquipParam());
		}
	}
    private void AddNowEquipNum(int addnum)//addnum分武器をずらす。マイナスにも対応
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
