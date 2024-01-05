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
	[Tooltip("���r�B�t���K�i")]
	public string ruby;
	public string name;
	[TextArea, Tooltip("������")]
	public string explanation;
	[Header("�X�e�[�^�X")]
	public int damage;
	public SizeEnum damageInUI;
	[Tooltip("�\���̑̐��ɓ���܂ł̑��x")]
	public int readyForSwingSpeed;
	public SpeedEnum readyForSwingSpeedInUI;
	[Tooltip("�U��ۂ̑��x�B���̋�ԃ_���[�W������")]
	public int swingSpeed;
	public SpeedEnum swingSpeedInUI;
	[Tooltip("�_���[�W��^�����ۂ̃m�b�N�o�b�N�̋���")]
	public int knockbackPower;
	public SizeEnum knockbackPowerInUI;
	[Tooltip("�H�����̑��x")]
	public int eatSpeed;
	public SpeedEnum eatSpeedInUI;
	[Tooltip("�H���I�����̉񕜗�")]
	public int healAmount;
	public SizeEnum healAmountInUI;
	public Vector3 offset;
}
