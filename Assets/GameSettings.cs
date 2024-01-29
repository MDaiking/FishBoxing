using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Setting
{
    public bool isRespawnInstance;
    public int maxhp;
}
public class GameSettings : MonoBehaviour
{
    public Setting setting;
}
