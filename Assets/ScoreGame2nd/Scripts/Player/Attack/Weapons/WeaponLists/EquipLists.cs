using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquipLists", menuName = "ScriptableObjects/CreateEnemyParam")]
public class EquipLists : ScriptableObject
{
	public List<EquipParam> equipParamList = new List<EquipParam>();
}

[System.Serializable]
public enum SizeEnum
{
	verySmall, small, midium, big, veryBig,
}
[System.Serializable]
public enum SpeedEnum
{
	verySlow, slow, midium, fast, veryFast,
}
[System.Serializable]
public class EquipParam
{
	public GameObject equipObject;
	public Sprite equipImage;
	[Tooltip("ルビ。フリガナ")]
	public string ruby;
	public string name;
	[TextArea, Tooltip("説明文")]
	public string explanation;
	[Header("ステータス")]
	public int damage;
	public SizeEnum damageInUI;
	[Tooltip("構えの体制に入るまでの速度")]
	public int readyForSwingSpeed;
	public SpeedEnum readyForSwingSpeedInUI;
	[Tooltip("振る際の速度。この区間ダメージが入る")]
	public int swingSpeed;
	public SpeedEnum swingSpeedInUI;
	[Tooltip("ダメージを与えた際のノックバックの強さ")]
	public int knockbackPower;
	public SizeEnum knockbackPowerInUI;
	[Tooltip("食事時の速度")]
	public int eatSpeed;
	public SpeedEnum eatSpeedInUI;
	[Tooltip("食事終了時の回復量")]
	public int healAmount;
	public SizeEnum healAmountInUI;
	public Vector3 offset;
}
