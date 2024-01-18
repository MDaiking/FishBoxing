using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]

public class PlayerInputList : MonoBehaviour//PIL = PlayerInputList
{
	private PlayerInput playerInput;

	private InputAction attack;
	private bool isAttack;
	public bool IsAttack
	{
		get { return isAttack; }
	}

	private bool isAttackStart;
	public bool IsAttackStart
	{
		get { return isAttackStart; }
	}

	private bool isAttackEnd;
	public bool IsAttackEnd
	{
		get { return isAttackEnd; }
	}

	private InputAction eat;
	private bool isEat;
	public bool IsEat
	{
		get { return isEat; }
	}

	private bool isEatStart;
	public bool IsEatStart
	{
		get { return isEatStart; }
	}

	private bool isEatEnd;
	public bool IsEatEnd
	{
		get { return isEatEnd; }
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
		eat = playerInput.actions["Eat"];
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
	//InputSystemÇ©ÇÁéÛÇØéÊÇÈì¸óÕêMçÜÇçXêV
	private void UpdateInput()
	{
		isAttack = (attack.ReadValue<float>() >= GameManager.threshold);
		isAttackStart = attack.WasPressedThisFrame();
		isAttackEnd = attack.WasReleasedThisFrame();
		isEat = (eat.ReadValue<float>() >= GameManager.threshold);
		isEatStart = eat.WasPressedThisFrame();
		isEatEnd = eat.WasReleasedThisFrame();
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