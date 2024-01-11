using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInputList), typeof(CharacterController), typeof(Animator))]
[RequireComponent(typeof(UseWeapon))]

public class PlayerMove : Unity.Netcode.NetworkBehaviour
{
	private PlayerInputList playerInputList;
	[SerializeField]//�ړ����x
	private float moveSpeed;
	[SerializeField]//��ގ��̌�������
	private float backDeboost;
	[SerializeField]//���Ⴊ�ݎ��̌�������
	private float crouchDeboost;
	[SerializeField]//����\�����̌�������
	private float readyToAttackDeboost;
	private bool isCrouch;
	private bool enableIsCrouch;//���Ⴊ�ނ��Ƃ��\���B�W�����v����false
	[SerializeField]
	private float jumpPower = 5.0f;

	private CharacterController controller;
	private Vector3 velocity;

	private UseWeapon useWeapon;
	private Animator animator;
	//[SerializeField]
	//private Animator armAnimator;//��l�̎��̂ݕ\�������r�̃A�j���[�^�[

	private float verticalVelocity;//�������x

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
	private void CalcMove()//�������v�Z
	{
		velocity = Vector3.zero;
		if (playerInputList.MoveAxis.sqrMagnitude >= GameManager.threshold)//�����Ă���Ƃ��̏���
		{
			velocity = (transform.forward * playerInputList.MoveAxis.y + transform.right * playerInputList.MoveAxis.x).normalized * moveSpeed;
			if (isCrouch)//���Ⴊ�ݎ��Ay�ȊO������
			{
				velocity *= crouchDeboost;
			}
			if (playerInputList.MoveAxis.y <= 0f)//��ގ��͌���
			{
				velocity *= backDeboost;
			}
			if (!useWeapon.IsIdleAnimation())//�r�̏�Ԃ�idle�ȊO�̏ꍇ(����\���Ȃ�)����
			{
				velocity *= readyToAttackDeboost;
			}
		}
		JumpAndGravity();

		//velocity = new Vector3(Mathf.Round(velocity.x), Mathf.Round(velocity.y), Mathf.Round(velocity.z));
	}

	//�A�j���[�V�����̑J��
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
		if (controller.isGrounded)//�n�ʂɂ��Ă���Ƃ�
		{
			enableIsCrouch = true;//���Ⴊ�݉\
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
			enableIsCrouch = false;//���Ⴊ�ݕs��

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
