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
	[Header("SE")]
	public AudioClip swingSound;
	public AudioClip hitSound;
	public AudioClip eatSound;
	[Header("�X�e�[�^�X")]
	public int damage;
	public SizeEnum damageInUI;
	[Tooltip("�\���̑̐��ɓ���܂ł̑��x")]
	public float readyForSwingSpeed;
	public SpeedEnum readyForSwingSpeedInUI;
	[Tooltip("�U��ۂ̑��x�B���̋�ԃ_���[�W������")]
	public float swingSpeed;
	public SpeedEnum swingSpeedInUI;
	[Tooltip("�_���[�W��^�����ۂ̃m�b�N�o�b�N�̋���")]
	public float knockbackPower;
	public float knockbackResistance;
	public SizeEnum knockbackPowerInUI;
	[Tooltip("�H�����̑��x")]
	public float eatSpeed;
	public SpeedEnum eatSpeedInUI;
	[Tooltip("�H���I�����̉񕜗�")]
	public int healAmount;
	public SizeEnum healAmountInUI;
	[Header("���̑��ݒ�")]
	public float defaultSize = 1.0f;
	public float atAttackSize = 1.0f;
	public Vector3 offset;
}
