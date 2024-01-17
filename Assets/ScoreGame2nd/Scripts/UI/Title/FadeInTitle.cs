using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(Fade),typeof(Image))]

public class FadeInTitle : MonoBehaviour
{
    private Attention attention;
    private Fade fade;
    private Tween fadeTween;
    private Tween attentionTween;
    private Image image;
    private Color imageColor;
    [SerializeField]
    private float fadeSpeed = 0.7f;
    void Start()
    {
        fade = GetComponent<Fade>();
        attention = transform.GetChild(0).GetComponent<Attention>();
        image = GetComponent<Image>();
        fade.InitAlpha(1.0f);
        attention.ShowAttention();
        imageColor = image.color;
    }
    void Update()
    {
        image.color = new Color(imageColor.r,imageColor.g,imageColor.b,fade.Alpha);
		if (!attention.IsShow)
		{
            fade.Fadeout(fadeSpeed);
		}
    }
}
