using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoyHPBar : MonoBehaviour
{
	private Slider slider;
	[SerializeField]
	private DecoyStatus decoyStatus;

	private Transform mainCamera;

	private void Start()
	{
		slider = GetComponent<Slider>();
		mainCamera = GameObject.FindWithTag("MainCamera").transform;
	}
	private void Update()
	{
		slider.value = decoyStatus.GetHPpc();
	}
	private void LateUpdate()
	{
		Vector3 lookVector = transform.position - mainCamera.position;//カメラへのベクトル
		transform.rotation = Quaternion.LookRotation(lookVector);
	}
}
