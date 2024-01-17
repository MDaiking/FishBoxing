using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
[RequireComponent(typeof(Fade), typeof(TextMeshProUGUI), typeof(Timer))]

public class Attention : MonoBehaviour
{
	private Fade fade;
	private SystemInputList systemInputList;
	private TextMeshProUGUI tmPro;
	private Color defaultColor;
	[SerializeField]
	private float fadeSpeed = 1.0f;
	[SerializeField]
	private float showTime = 3.0f;
	private Tween fadeinTween;
	private Tween fadeoutTween;
	private bool isShow;
	private bool isHiding;
	private Timer timer;
	public bool IsShow
	{
		get { return isShow; }
	}
	private void Start()
	{
		fade = GetComponent<Fade>();
		tmPro = GetComponent<TextMeshProUGUI>();
		timer = GetComponent<Timer>();
		systemInputList = GameObject.FindWithTag("GameManager").GetComponent<SystemInputList>();
		defaultColor = tmPro.color;
		fade.InitAlpha(0.0f);
	}
	private void Update()
	{
		tmPro.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, fade.Alpha);
		if (IsShow && !isHiding && (systemInputList.IsAnyKey || timer.GetTimePercent() >= 1.0f))//画面表示中、キー入力か一定時間経過で自動フェード
		{
			HideAttention();
		}
	}
	public void ShowAttention()
	{
		if (fade == null)
		{
			fade = GetComponent<Fade>();
		}
		if (timer == null)
		{
			timer = GetComponent<Timer>();
		}
		isShow = true;

		fadeinTween = fade.Fadein(fadeSpeed)
			.OnComplete(() => { timer.StartTimer(showTime); });
	}
	private void HideAttention()
	{
		fadeinTween.Kill();
		fadeoutTween = fade.Fadeout(fadeSpeed)
			.OnStart(() => { isHiding = true; })
			.OnComplete(() => { isShow = false; isHiding = false; });
	}

}
