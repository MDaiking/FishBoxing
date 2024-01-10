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
	/// スタートできればtrue,既に実行中で出来ない場合はfalseを返す
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
	/// 停止時、開始したときに設定した時間まで到達していればtrue,それ以外はfalse
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
