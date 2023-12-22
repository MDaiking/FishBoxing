using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputList : MonoBehaviour//PIL = PlayerInputList
{
	private PlayerInput playerInput;

	private InputAction attack;
	private bool canAttack;
	private bool isAttackStart;

	public bool GetCanAttack
	{
		get { return canAttack; }
	}
	public bool StartToGetAttack
	{
		get { return isAttackStart; }
	}

	private InputAction move;
	private Vector2 moveAxis;
	public Vector2 MoveAxis
	{
		get { return moveAxis; }
	}

	private InputAction jump;
	private bool isJump;
	public bool IsJump
	{
		get { return isJump; }
	}

	private InputAction crouch;
	private bool isCrouch;
	public bool IsCrouch
	{
		get { return isCrouch; }
	}

	private InputAction switchWeapon;
	private float switchWeaponAxisY;
	public float SwitchWeaponAxis
	{
		get{ return switchWeaponAxisY; }
	}
	private InputAction option;
	private bool isOption;
	private bool isOptionStart;
	public bool IsOption
	{
		get{ return isOption; }
	}
	public bool IsOptionStart
	{
		get{ return isOptionStart; }
	}


	private void Start()
	{
		playerInput = GetComponent<PlayerInput>();

		attack = playerInput.actions["Attack"];
		move = playerInput.actions["Move"];
		jump = playerInput.actions["Jump"];
		crouch = playerInput.actions["Crouch"];
		switchWeapon = playerInput.actions["SwitchWeapon"];
		option = playerInput.actions["Option"];
	}
	private void Update()
	{
		UpdateInput();
	}
	//InputSystem����󂯎����͐M�����X�V
	private void UpdateInput()
	{
		canAttack = (attack.ReadValue<float>() >= GameManager.threshold);
		isAttackStart = attack.WasPressedThisFrame();
		moveAxis = move.ReadValue<Vector2>();
		isJump = (jump.ReadValue<float>() >= GameManager.threshold);
		isCrouch = (crouch.ReadValue<float>() >= GameManager.threshold);
		switchWeaponAxisY = switchWeapon.ReadValue<float>();
		isOption = (option.ReadValue<float>() >= GameManager.threshold);
		isOptionStart = option.WasPressedThisFrame();

	}
	public bool IsCurrentDeviceMouse
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