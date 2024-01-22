using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowToggleInScene : MonoBehaviour
{
	private List<GameObject> children = new List<GameObject>();
	[SerializeField]
	private GameSceneEnum gameSceneEnum;
	private GameSceneEnum beforeGameSceneEnum;
	private GameScene gameScene;
	private void Start()
	{
		gameScene = GameObject.FindWithTag("GameController").GetComponent<GameScene>();
		foreach (Transform child in transform)
		{
			children.Add(child.gameObject);
		}
		beforeGameSceneEnum = gameScene.NowGameScene;
	}
	private void Update()
	{
		if (beforeGameSceneEnum != gameScene.NowGameScene)
		{
			beforeGameSceneEnum = gameScene.NowGameScene;
			foreach (GameObject child in children)
			{
				child.SetActive(beforeGameSceneEnum == gameSceneEnum);
			}
		}
	}

}
