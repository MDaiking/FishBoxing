using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class TitleController : MonoBehaviour
{
    GameObject title;
    List<GameObject> children = new List<GameObject>();

    CanvasGroup canvasGroup;
    [SerializeField]
    private TitleMenuController titleButtons;
    void Start()
    {
        title = GameObject.FindWithTag("Title");
        canvasGroup = GetComponent<CanvasGroup>();
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
