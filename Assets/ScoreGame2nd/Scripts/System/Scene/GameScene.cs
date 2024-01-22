using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GameSceneEnum
{
	buy, ready, goOn, finish
}

[RequireComponent(typeof(Timer))]
public class GameScene : MonoBehaviour
{
	private Timer timer;
	private GameSceneEnum nowGameScene;
	private GameSceneEnum reservingGameScene;
	public GameSceneEnum NowGameScene
	{
		get { return nowGameScene; }
		set 
		{
			if (timer.IsActivated)
			{
				Debug.LogWarning("タイマーが起動中にゲーム内シーン変更が行われました");
				timer.StopTimer();
			}
			nowGameScene = value; 
		}
	}
	private void Start()
	{
		timer = GetComponent<Timer>();
	}
	private void Update()
	{
		if (timer.IsActivated && (timer.GetTimePercent() >= 1.0f))
		{
			timer.StopTimer();
			NowGameScene = reservingGameScene;
			
		}
	}
	public void SetTimerForTheNextScene(GameSceneEnum gameScene, float sec)
	{
		timer.StartTimer(sec);
		reservingGameScene = gameScene;
	}

}
