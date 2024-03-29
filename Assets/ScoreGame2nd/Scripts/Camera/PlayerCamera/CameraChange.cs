using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraChange : Unity.Netcode.NetworkBehaviour
{
	private CinemachineVirtualCamera fpsCamera;
	private CinemachineVirtualCamera tpsCamera;
	public CinemachineVirtualCamera FpsCamera
	{
		set
		{
			if (fpsCamera == null)
				fpsCamera = value;
		}
	}
	public CinemachineVirtualCamera TpsCamera
	{
		set
		{
			if (tpsCamera == null)
				tpsCamera = value;
		}
	}

	private PlayerInput playerInput;
	private InputAction switchCamera;
	private PlayerInvisible playerInvisible;

	[SerializeField]//数字が大きいほどカメラ優先度が高い
	private int activePriority = 10;
	[SerializeField]
	private int inactivePriority = 1;
	public int ActivePriority
	{
		get
		{
			return activePriority;
		}
	}
	public int InactivePriority
	{
		get
		{
			return inactivePriority;
		}
	}

	private bool doesAlreadyPush;
	private void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		switchCamera = playerInput.actions["SwitchCamera"];
		playerInvisible = GetComponent<PlayerInvisible>();
		SetPriority(false);
	}

	private void Update()
	{
		if (IsOwner)
		{
			//CheckSwitchCamera();
		}
	}
	private void CheckSwitchCamera()//FPSとTPSを切り替えるかを監視
	{
		//if (IsCameraSwitch())
		//{
		//	if (!doesAlreadyPush)
		//	{
		//		doesAlreadyPush = true;
		//		SwitchPriority();
		//	}
		//}
		//else
		//{
		//	doesAlreadyPush = false;
		//}
	}
	private bool IsCameraSwitch()
	{
		float switchCameraFloat = switchCamera.ReadValue<float>();
		return (switchCameraFloat > 0.0f);
	}
	private void SwitchPriority()
	{
		if (fpsCamera.Priority == activePriority)
		{
			fpsCamera.Priority = inactivePriority;
			tpsCamera.Priority = activePriority;
			playerInvisible.SetInvisible(false);
		}
		else
		{
			fpsCamera.Priority = activePriority;
			tpsCamera.Priority = inactivePriority;
			playerInvisible.SetInvisible(true);
		}
	}
	private void SetPriority(bool isFps)
	{
		if (!isFps)
		{
			fpsCamera.Priority = inactivePriority;
			tpsCamera.Priority = activePriority;
			playerInvisible.SetInvisible(false);
		}
		else
		{
			fpsCamera.Priority = activePriority;
			tpsCamera.Priority = inactivePriority;
			playerInvisible.SetInvisible(true);
		}
	}
}
