using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInputList), typeof(CharacterController), typeof(Animator))]
[RequireComponent(typeof(UseWeapon))]

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
	private bool enableIsCrouch;//しゃがむことが可能か。ジャンプ時にfalse
	[SerializeField]
	private float jumpPower = 5.0f;

	private CharacterController controller;
	private Vector3 velocity;

	private UseWeapon useWeapon;
	private Animator animator;
	//[SerializeField]
	//private Animator armAnimator;//一人称時のみ表示される腕のアニメーター

	private float verticalVelocity;//垂直速度

	[SerializeField]
	private CameraRootMove cameraRoot;

	private void Start()
	{
		if (IsOwner)
		{
			playerInputList = GetComponent<PlayerInputList>();
			controller = GetComponent<CharacterController>();
			animator = GetComponent<Animator>();
			useWeapon = GetComponent<UseWeapon>();
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
		}
	}
	[Unity.Netcode.ServerRpc]
	private void SetMoveInputServerRpc(Vector3 _velocity, float _verticalVelocity)
	{
		this.velocity = _velocity;
		this.verticalVelocity = _verticalVelocity;
	}
	private void FixedUpdate()
	{
		if (IsOwner)
		{
			controller.Move(velocity * Time.deltaTime + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
		}
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
		JumpAndGravity();

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

	private void JumpAndGravity()
	{
		if (controller.isGrounded)//地面についているとき
		{
			enableIsCrouch = true;//しゃがみ可能
			if (verticalVelocity < 0.0f)
			{
				verticalVelocity = -0.0f;
			}
			if (playerInputList.IsJump)
			{
				verticalVelocity = jumpPower;
			}
		}
		else
		{
			enableIsCrouch = false;//しゃがみ不可

		}
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
	}
	private void ToggleCrouch()
	{
		if (enableIsCrouch)
		{
			isCrouch = playerInputList.IsCrouch;
		}
	}
}


/*
		Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
		inputDirection = cameraForward * inputDirection.x + mainCamera.transform.right * inputDirection.z;
		inputDirection.y = 0.0f; 
 * */
