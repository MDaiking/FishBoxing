using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Unity.Netcode.NetworkBehaviour
{
	private PlayerInputList pil;
	[SerializeField]//移動速度
	private float moveSpeed;
	[SerializeField]//後退時の減衰割合
	private float backDeboost;
	[SerializeField]//しゃがみ時の減衰割合
	private float crouchDeboost;
	private bool isCrouch;
	private bool enableIsCrouch;//しゃがむことが可能か。ジャンプ時にfalse
	[SerializeField]
	private float jumpPower = 5.0f;

	private CharacterController controller;
	private Vector3 velocity;

	private Animator animator;
	[SerializeField]
	//private Animator armAnimator;//一人称時のみ表示される腕のアニメーター

	private float verticalVelocity;//垂直速度

	[SerializeField]
	private CameraRootMove cameraRoot;

	private void Start()
	{
		if (IsOwner)
		{
			pil = GetComponent<PlayerInputList>();
			controller = GetComponent<CharacterController>();
			animator = GetComponent<Animator>();
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
		if (pil.MoveAxis.sqrMagnitude >= GameManager.threshold)//動いているときの処理
		{
			velocity = (transform.forward * pil.MoveAxis.y + transform.right * pil.MoveAxis.x).normalized * moveSpeed;
			if (isCrouch)//しゃがみ時、y以外を減速
			{
				velocity *= crouchDeboost;
			}
			if (pil.MoveAxis.y <= 0f)//後退時は減速
			{
				velocity *= backDeboost;
			}
		}
		JumpAndGravity();

		//velocity = new Vector3(Mathf.Round(velocity.x), Mathf.Round(velocity.y), Mathf.Round(velocity.z));
	}

	//アニメーションの遷移
	private void MoveAnimation()
	{
		animator.SetFloat("ZAxis", pil.MoveAxis.y);
		animator.SetFloat("XAxis", pil.MoveAxis.x);
		animator.SetBool("IsCrouch", isCrouch);
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
			if (pil.IsJump)
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
			isCrouch = pil.IsCrouch;
		}
	}
}


/*
		Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
		inputDirection = cameraForward * inputDirection.x + mainCamera.transform.right * inputDirection.z;
		inputDirection.y = 0.0f; 
 * */
