using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(RectTransform))]

public class SelectWeaponParent : MonoBehaviour
{
	[SerializeField] CursorController cursorController;
	[SerializeField] float moveTimeToShow;
	[SerializeField] float moveTimeToHide;
	RectTransform rectTransform;

	bool testBool;
	bool canMove;
	private void Start()
	{
		testBool = false;
		canMove = true;
		rectTransform = GetComponent<RectTransform>();
		InstantToggleUI(testBool);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B) && canMove)
		{
			testBool = !testBool;
			ToggleUI(testBool);
		}
	}
	private void ToggleUI(bool value)
	{
		if (value)
		{
			SetActiveAllChildren(true);
			cursorController.IsCursorShow = true;
			rectTransform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), moveTimeToShow).SetEase(Ease.OutBounce)
				.OnStart(()=>{ canMove = false; })
				.OnComplete(()=> { canMove = true; });
		}
		else
		{
			int screenWidth = Screen.height;
			cursorController.IsCursorShow = false;
			rectTransform.DOLocalMove(new Vector3(0.0f, screenWidth, 0.0f), moveTimeToHide).SetEase(Ease.OutQuad)
				.OnStart(()=>{ canMove = false; })
				.OnComplete(() => { SetActiveAllChildren(false); canMove = true; });
		}
	}
	private void InstantToggleUI(bool value)
	{
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
