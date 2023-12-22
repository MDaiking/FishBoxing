using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipLists", menuName = "ScriptableObjects/CreateEnemyParam")]
public class EquipLists : ScriptableObject
{
    public List<EquipParam> equipParamList = new List<EquipParam>();
}

[System.Serializable]
public class EquipParam
{
    public GameObject equipObject;
    public string name;
    public int damage;
    public int swingSpeed;
    public Vector3 offset;
}
