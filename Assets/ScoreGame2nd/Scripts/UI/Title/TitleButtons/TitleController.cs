using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(CanvasGroup),typeof(Fade))]

public class TitleController : MonoBehaviour
{
    Fade fade;
    List<GameObject> children = new List<GameObject>();

    CanvasGroup canvasGroup;
    [SerializeField]
    private TitleMenuController titleButtons;
    void Start()
    {
        fade = GetComponent<Fade>();
        canvasGroup = GetComponent<CanvasGroup>();
        foreach(Transform child in transform)
		{
            children.Add(child.gameObject);
		}
    }
    void Update()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.alpha = fade.Alpha;


    }

    public void ToggleTitleActive(bool active)
	{
        foreach(GameObject child in children)
		{
            child.SetActive(active);
		}
		if (active)
		{
            fade.Fadein(1.0f);
		}
	}
}
