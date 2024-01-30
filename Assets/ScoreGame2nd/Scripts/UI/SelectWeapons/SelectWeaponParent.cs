using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(RectTransform), typeof(AudioSource))]

public class SelectWeaponParent : MonoBehaviour
{
	private PlayerEquips playerEquips;
	public PlayerEquips PlayerEquip
	{
		get
		{
			return playerEquips;
		}
		set
		{
			if (PlayerEquip == null)
			{
				playerEquips = value;
			}
		}
	}
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip shopSound;
	[SerializeField]
	private CursorController cursorController;
	[SerializeField]
	private float moveTimeToShow;
	[SerializeField]
	private float moveTimeToHide;
	private RectTransform rectTransform;

	private bool isShow = false;
	public bool IsShow
	{
		get { return isShow; }
	}

	bool canMove;
	private void Start()
	{
		canMove = true;
		rectTransform = GetComponent<RectTransform>();
		audioSource = GetComponent<AudioSource>();
		InstantToggleUI(false);
		ToggleUI(true);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B) && canMove)
		{
			isShow = !isShow;
			ToggleUI(isShow);
		}
	}
	public void ToggleUI(bool value)
	{
		isShow = value;
		if (value)
		{
			audioSource.PlayOneShot(shopSound);
			SetActiveAllChildren(true);
			cursorController.IsCursorShow = true;
			rectTransform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), moveTimeToShow).SetEase(Ease.OutBounce)
				.OnStart(() => { canMove = false; })
				.OnComplete(() => { canMove = true; });
		}
		else
		{
			int screenHeight = Screen.height;
			cursorController.IsCursorShow = false;
			rectTransform.DOLocalMove(new Vector3(0.0f, screenHeight, 0.0f), moveTimeToHide).SetEase(Ease.OutQuad)
				.OnStart(() => { canMove = false; })
				.OnComplete(() => { SetActiveAllChildren(false); canMove = true; });
		}
	}
	private void InstantToggleUI(bool value)
	{
		isShow = value;
		if (value)
		{
			SetActiveAllChildren(true);
			rectTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
		}
		else
		{
			int screenWidth = Screen.height;
			rectTransform.DOLocalMove(new Vector3(0.0f, screenWidth, 0.0f), moveTimeToHide);
			SetActiveAllChildren(false);
		}
	}
	private void SetActiveAllChildren(bool value)
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(value);
		}
	}
}
