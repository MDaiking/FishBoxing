using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : Unity.Netcode.NetworkBehaviour
{
	[SerializeField]
	private float rotateSpeed;
	public float RotateSpeed{
		get{ return rotateSpeed; }
		set{ rotateSpeed = value; }
	}
	private PlayerInput playerInput;
	private InputAction rotate;
	private Vector2 rotateAxis;
	[SerializeField]
	private GameObject cameraRoot;
	[SerializeField]
	private float bottomClamp = -30.0f;
	[SerializeField]
	private float topClamp = 70.0f;
	[SerializeField]
	private float CameraAngleOverride = 0.0f;
	private float pitch;//�c��]�B�����㉺�ɐU��܂ł͓��
	private float yaw;//����]�B�L���������ۂɂ��̕���������

	private const float threshold = 0.01f;

	private Quaternion rotateCharacter;
	private Quaternion rotateCamera;
	//[SerializeField]
	//private RotateText rotateText;
	private void Start()
	{
		if (IsOwner)
		{
			rotateCharacter = transform.localRotation;
			playerInput = GetComponent<PlayerInput>();
			rotate = playerInput.actions["Rotate"];
			//rotateText = GameObject.Find("rotate").GetComponent<RotateText>();
		}
	}

	private void Update()
	{
		if (IsOwner)
		{
			rotateAxis = rotate.ReadValue<Vector2>();
			CalcPitch();
			CalcYaw();
			SetRotateInputServerRpc(pitch, yaw);
			//rotateText.Rotate = rotateAxis.x;
		}
	}
	[Unity.Netcode.ServerRpc]
	private void SetRotateInputServerRpc(float _pitch, float _yaw)//client���Ōv�Z�����������T�[�o�[���ɓ����
	{
		this.pitch = _pitch;
		this.yaw = _yaw;
	}
	private void FixedUpdate()
	{
		RotateUpdate();
	}
	private void RotateUpdate()
	{
		if (IsOwner)
		{
			//�@�L�����N�^�[�̉�]�����s
			transform.rotation = Quaternion.Euler(0.0f, pitch + CameraAngleOverride, 0.0f);
			//�@�J�����̉�]�����s
			cameraRoot.transform.rotation = Quaternion.Euler(yaw, pitch + CameraAngleOverride, 0.0f);
		}

	}
	private void CalcPitch()
	{
		//�@���̉�]�l���v�Z
		if (rotateAxis.sqrMagnitude >= threshold)
		{
			//Don't multiply mouse input by Time.deltaTime;
			float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
			pitch += rotateAxis.x * deltaTimeMultiplier * rotateSpeed;
		}
		pitch = ClampAngle(pitch, float.MinValue, float.MaxValue);
	}
	private void CalcYaw()
	{

		if (rotateAxis.sqrMagnitude >= threshold)
		{
			//Don't multiply mouse input by Time.deltaTime;
			float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

			yaw += rotateAxis.y * deltaTimeMultiplier * rotateSpeed;
		}
		yaw = ClampAngle(yaw, bottomClamp, topClamp);
	}
	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}
	//test
	private bool IsCurrentDeviceMouse
	{
		get
		{
#if ENABLE_INPUT_SYSTEM
			return playerInput.currentControlScheme == "KeyBoardMouse";
#else
				return false;
#endif
		}
	}
}