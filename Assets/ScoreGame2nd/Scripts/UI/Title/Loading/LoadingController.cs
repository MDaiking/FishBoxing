using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
	List<GameObject> children = new List<GameObject>();
	[SerializeField]
	private ProgressBarUI progressUI;

	private void Start()
	{
		foreach (Transform child in transform)
		{
			children.Add(child.gameObject);
		}
		SetActiveChildren(false);
	}
	public void LoadNextScene(string loadSceneName)
	{
		SetActiveChildren(true);
		StartCoroutine(LoadScene(loadSceneName));
	}
	IEnumerator LoadScene(string loadSceneName)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName);
		while (!async.isDone)
		{
			progressUI.SetPercentage(async.progress);
			yield return null;
		}
	}
	private void SetActiveChildren(bool active)
	{
		foreach (GameObject child in children)
		{
			child.SetActive(active);
		}
	}
}
