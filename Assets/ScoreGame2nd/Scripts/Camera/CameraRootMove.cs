using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRootMove : MonoBehaviour
{
	[SerializeField]
	private float speed;
	private float crouchY;
	private float defaultY;
	private bool isCrouch;//ÇµÇ·Ç™Ç›
	private bool isChangeToCrouch;
	private PlayerDeath playerDeath;
	[SerializeField]
	private Transform deadWatchingPos;

	public bool IsCrouch
	{
		set
		{
			if (isCrouch != value)
			{
				isChangeToCrouch = true;
				isCrouch = value;
			}
		}
	}

	private void Start()
	{
		defaultY = transform.localPosition.y;
		crouchY = defaultY / 2.0f;

		isCrouch = false;
		isChangeToCrouch = false;
		playerDeath = transform.parent.GetComponent<PlayerDeath>();

	}
	private void Update()
	{
		if (!playerDeath.IsPlayerDead)
		{
			CrouchMove();
		}
		else
		{
			transform.position = deadWatchingPos.position;
		}
	}
	private void CrouchMove()
	{
		if (isChangeToCrouch)
		{
			isChangeToCrouch = false;
			if (isCrouch)
			{
				transform.DOLocalMoveY(crouchY, speed)
				.SetEase(Ease.OutQuad);//í èÌ->ÇµÇ·Ç™Ç›ÇÃìÆÇ´
			}
			else
			{
				transform.DOLocalMoveY(defaultY, speed)
				.SetEase(Ease.OutQuad);//ÇµÇ·Ç™Ç›->í èÌÇÃìÆÇ´
			}
		}

	}
}
