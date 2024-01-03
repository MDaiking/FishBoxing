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
public class EquipParam
{
    public GameObject equipObject;
    public Sprite equipImage;
    [Tooltip("���r�B�t���K�i")]
    public string ruby;
    public string name;
    [TextArea,Tooltip("������")]
    public string explanation;
    [Header("�uInUI�v�̂����̂́A���Љ��UI�ɂĕ\�����������p�\�L")]
    public int damage;
    public string damageInUI;
    [Tooltip("�\���̑̐��ɓ���܂ł̑��x")]
    public int readyForSwingSpeed;
    public string readyForSwingSpeedInUI;
    [Tooltip("�U��ۂ̑��x�B���̋�ԃ_���[�W������")]
    public int swingSpeed;
    public string swingSpeedInUI;
    [Tooltip("�_���[�W��^�����ۂ̃m�b�N�o�b�N�̋���")]
    public int knockbackPower;
    public string knockbackPowerInUI;
    [Tooltip("�H�����̑��x")]
    public int eatSpeed;
    public string eatSpeedInUI;
    [Tooltip("�H���I�����̉񕜗�")]
    public int healAmount;
    public string healAmountInUI;
    public Vector3 offset;
}
