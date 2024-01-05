using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AboutWeaponController : MonoBehaviour
{
    [SerializeField]
    EquipLists equipLists;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI ruby;
    [SerializeField]
    new private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI explanation;
    [SerializeField]
    private ShowStatus status;

    int showWeaponNum;
	private void Start()
	{
        showWeaponNum = -1;
        SetWeaponStatus(-1);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
            showWeaponNum++;
            if (showWeaponNum >= 5) showWeaponNum = -1;
            SetWeaponStatus(showWeaponNum);
		}
	}
	private void SetWeaponStatus(int num)
	{
        if(num == -1)
		{
            image.sprite = null;
            ruby.text = "";
            name.text = "";
            explanation.text = "";
            status.HideAllStatusUI();
		}
		else
		{
            EquipParam equipParam = equipLists.equipParamList[num];
            image.sprite = equipParam.equipImage;
            ruby.text = equipParam.ruby;
            name.text = equipParam.name;
            explanation.text = equipParam.explanation;
            status.SetTexts(equipParam);
		}
	}
}
