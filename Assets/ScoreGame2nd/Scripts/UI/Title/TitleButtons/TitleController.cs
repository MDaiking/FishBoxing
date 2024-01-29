using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();
    [SerializeField]
    private TitleMenuController titleButtons;
    void Start()
    {
        foreach(Transform child in transform)
		{
            children.Add(child.gameObject);
		}
    }
    void Update()
    {
        
    }
    public void ToggleTitleActive(bool active)
	{
        foreach(GameObject child in children)
		{
            child.SetActive(active);
		}
	}
}
