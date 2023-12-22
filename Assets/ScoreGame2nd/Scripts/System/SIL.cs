using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SIL : MonoBehaviour//SIL = SystemInputList
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


	private void Start()
	{
		systemInput = GetComponent<PlayerInput>();

		option = systemInput.actions["Option"];
	}
	private void Update()
	{
		UpdateInput();
	}
	//InputSystem����󂯎����͐M�����X�V
	private void UpdateInput()
	{
		isOption = (option.ReadValue<float>() >= GameManager.threshold);
		isOptionStart = option.WasPressedThisFrame();

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