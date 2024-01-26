using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInputList), typeof(Rigidbody), typeof(Animator))]
[RequireComponent(typeof(UseWeapon), typeof(PlayerKnockback),typeof(PlayerDeath))]

public class PlayerMove : Unity.Netcode.NetworkBehaviour
{
	private PlayerInputList playerInputList;
	[SerializeField]//移動速度
	private float moveSpeed;
	[SerializeField]//後退時の減衰割合
	private float backDeboost;
	[SerializeField]//しゃがみ時の減衰割合
	private float crouchDeboost;
	[SerializeField]//武器構え時の減衰割合
	private float readyToAttackDeboost;
	private bool isCrouch;
	public bool IsCrouch
	{
		get { return isCrouch; }
	}
	[SerializeField]
	private float crouchResistance = 2.0f;
	public float CrouchResistance
	{
		get { return crouchResistance; }
	}
	private bool enableIsCrouch;//しゃがむことが可能か。ジャンプ時にfalse
	[SerializeField]
	private float jumpPower = 5.0f;

	private Rigidbody rb;
	private Vector3 velocity;

	private UseWeapon useWeapon;
	private Animator animator;
	//[SerializeField]
	//private Animator armAnimator;//一人称時のみ表示される腕のアニメーター
	private PlayerKnockback playerKnockback;
	PlayerDeath playerDeath;

	private float verticalVelocity;//垂直速度

	[SerializeField]
	private CameraRootMove cameraRoot;

	private void Start()
	{
		if (IsOwner)
		{
			playerInputList = GetComponent<PlayerInputList>();
			rb = GetComponent<Rigidbody>();
			animator = GetComponent<Animator>();
			useWeapon = GetComponent<UseWeapon>();
			playerKnockback = GetComponent<PlayerKnockback>();
			playerDeath = GetComponent<PlayerDeath>();
			enableIsCrouch = true;
			isCrouch = false;
		}
	}

	private void Update()
	{
		if (IsOwner)
		{
			ToggleCrouch();
			CalcMove();
			SetMoveInputServerRpc(velocity, verticalVelocity);
			MoveAnimation();
			CameraRootMove();
			playerKnockback.IsCrouched = IsCrouch;
			if (transform.position.y <= 0.0f)
			{
				playerDeath.Death();
				rb.drag = CalcDrag(true);
			}
			rb.velocity = (velocity + new Vector3(0f, verticalVelocity, 0f) + playerKnockback.CalcKnockback());
		}
	}
	[Unity.Netcode.ServerRpc]
	private void SetMoveInputServerRpc(Vector3 _velocity, float _verticalVelocity)
	{
		this.velocity = _velocity;
		this.verticalVelocity = _verticalVelocity;
	}

	private void CameraRootMove()
	{
		if (isCrouch) cameraRoot.IsCrouch = true;
		else cameraRoot.IsCrouch = false;
	}
	private void CalcMove()//動きを計算
	{
		velocity = Vector3.zero;
		if (playerInputList.MoveAxis.sqrMagnitude >= GameManager.threshold)//動いているときの処理
		{
			velocity = (transform.forward * playerInputList.MoveAxis.y + transform.right * playerInputList.MoveAxis.x).normalized * moveSpeed;
			if (isCrouch)//しゃがみ時、y以外を減速
			{
				velocity *= crouchDeboost;
			}
			if (playerInputList.MoveAxis.y <= 0f)//後退時は減速
			{
				velocity *= backDeboost;
			}
			if (!useWeapon.IsIdleAnimation())//腕の状態がidle以外の場合(武器構えなど)減速
			{
				velocity *= readyToAttackDeboost;
			}
		}
		Gravity();

		//velocity = new Vector3(Mathf.Round(velocity.x), Mathf.Round(velocity.y), Mathf.Round(velocity.z));
	}

	//アニメーションの遷移
	private void MoveAnimation()
	{
		animator.SetFloat("ZAxis", playerInputList.MoveAxis.y);
		animator.SetFloat("XAxis", playerInputList.MoveAxis.x);
		animator.SetBool("IsCrouch", isCrouch);

		float weaponDeboost = useWeapon.IsIdleAnimation() ? 1.0f : readyToAttackDeboost;
		animator.SetFloat("UsingWeapon", weaponDeboost);

	}

	private void Gravity()
	{
		verticalVelocity = rb.velocity.y;
	}
	private void ToggleCrouch()
	{
		if (enableIsCrouch)
		{
			isCrouch = playerInputList.IsCrouch;
		}
	}
	private float CalcDrag(bool isInWater)
	{
		if (isInWater)
		{
			return 3.0f;
		}
		else
		{
			return 0.0f;
		}
	}
}


/*
		Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
		inputDirection = cameraForward * inputDirection.x + mainCamera.transform.right * inputDirection.z;
		inputDirection.y = 0.0f; 
 * */
