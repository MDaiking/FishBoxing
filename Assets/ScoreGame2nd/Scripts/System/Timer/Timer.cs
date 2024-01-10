using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private int time;
	private int timer;
	private bool isActivated;
	public float GetTimePercent()
	{
		return (float)time / timer;
	}
	private void Start()
	{
		time = 0;
		isActivated = false;
	}
	private void FixedUpdate()
	{
		if (isActivated)
		{
			++time;
		}
	}
	/// <summary>
	/// �X�^�[�g�ł����true,���Ɏ��s���ŏo���Ȃ��ꍇ��false��Ԃ�
	/// </summary>
	/// <param name="sec"></param>
	public bool StartTimer(float sec)
	{
		if (!isActivated)
		{
			timer = (int)(sec * Application.targetFrameRate);
			isActivated = true;

			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// ��~���A�J�n�����Ƃ��ɐݒ肵�����Ԃ܂œ��B���Ă����true,����ȊO��false
	/// </summary>
	/// <returns></returns>
	public bool StopTimer()
	{
		bool returnValue;
		returnValue = (time >= timer);
		isActivated = false;
		time = 0;
		timer = 0;

		return returnValue;
	}
}
