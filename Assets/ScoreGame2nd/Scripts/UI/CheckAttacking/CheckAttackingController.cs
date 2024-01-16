using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(CanvasGroup))]

public class CheckAttackingController : MonoBehaviour
{
	private CanvasGroup canvasGroup;
	private float alpha;
	[SerializeField]
	private float inactiveSpeed;
	private GameObject bgGO;
	private GameObject showAttackGO;
	private Image showAttackImage;
	private bool isActive;
	private float fillAmount;
	private Tween inactiveTween;
	[SerializeField]
	private float noTouchDecrease = 0.1f;
	private void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		bgGO = transform.GetChild(0).gameObject;
		showAttackGO = transform.GetChild(1).gameObject;
		showAttackImage = showAttackGO.GetComponent<Image>();
		fillAmount = 0.0f;
		alpha = 0.0f;
	}
	private void Update()
	{
		canvasGroup.alpha = alpha;
	}
	private void FixedUpdate()
	{
		showAttackImage.fillAmount = fillAmount;
	}
	public void SetAmount(float amount)
	{
		if (!isActive)
		{
			SetActive();
		}
		fillAmount = amount;

	}
	public void SetActive()
	{
		if (inactiveTween != null)
		{
			inactiveTween.Kill();
		}
		alpha = 1.0f;
		bgGO.SetActive(true);
		showAttackGO.SetActive(true);
		isActive = true;
	}
	public void SetInactive()
	{
		inactiveTween = DOTween.To(() => alpha, (x) => alpha = x, 0.0f, inactiveSpeed)
			.OnUpdate(() => { if (fillAmount > 0.0f) fillAmount -= noTouchDecrease; })
			.OnComplete(() =>
			{
				fillAmount = 0.0f;
				showAttackImage.fillAmount = 0.0f;
				bgGO.SetActive(false);
				showAttackGO.SetActive(false);
				isActive = false;
			});
	}
}
