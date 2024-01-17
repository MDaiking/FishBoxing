using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    private float alpha;
    public float Alpha
	{
		get{ return alpha; }
	}
	public void InitAlpha(float initValue)
	{
		alpha = initValue;
	}
	public Tween Fadein(float fadeSpeed)
	{
		return DOTween.To(() => alpha, (x) => alpha = x, 1.0f, fadeSpeed);
	}
	public Tween Fadeout(float fadeSpeed)
	{
		return DOTween.To(() => alpha, (x) => alpha = x, 0.0f, fadeSpeed);
	}
}
