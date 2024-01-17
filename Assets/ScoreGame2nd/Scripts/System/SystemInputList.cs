using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemInputList: MonoBehaviour
{
	private PlayerInput systemInput;

	private InputAction option;
	private bool isOption;
	private bool isOptionStart;
	public bool IsOption
	{
		get { return isOption; }
	}
	public bool IsOptionStart
	{
		get { return isOptionStart; }
	}
	private InputAction anyKey;
	private bool isAnyKey;
	public bool IsAnyKey
	{
		get{ return isAnyKey; }
	}


	private void Start()
	{
		systemInput = GetComponent<PlayerInput>();

		option = systemInput.actions["Option"];
		anyKey = systemInput.actions["AnyKey"];
	}
	private void Update()
	{
		UpdateInput();
	}
	//InputSystem‚©‚çó‚¯æ‚é“ü—ÍM†‚ğXV
	private void UpdateInput()
	{
		isOption = (option.ReadValue<float>() >= GameManager.threshold);
		isOptionStart = option.WasPressedThisFrame();
		isAnyKey = (anyKey.ReadValue<float>() >= GameManager.threshold);

	}
	public bool IsCurrentDeviceMouse
	{
		get
		{
#if ENABLE_INPUT_SYSTEM
			return systemInput.currentControlScheme == "KeyBoardMouse";
#else
				return false;
#endif
		}
	}
}