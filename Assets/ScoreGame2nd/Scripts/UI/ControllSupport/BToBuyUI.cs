using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]

public class BToBuyUI : MonoBehaviour
{
	[SerializeField]
	private SelectWeaponParent selectWeaponParent;
	private TextMeshProUGUI textMeshPro;
	private void Start()
	{
		textMeshPro = GetComponent<TextMeshProUGUI>();
	}
	private void Update()
	{
		if (selectWeaponParent.IsShow)
		{
			textMeshPro.alpha = 0.0f;//”ñ•\Ž¦
		}
		else
		{
			textMeshPro.alpha = 1.0f;//•\Ž¦
		}
	}
}
