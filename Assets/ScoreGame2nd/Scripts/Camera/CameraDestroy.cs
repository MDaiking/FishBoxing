using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDestroy : MonoBehaviour
{
	CinemachineVirtualCamera cvc;
	private void Start()
	{
		cvc = GetComponent<CinemachineVirtualCamera>();
	}
	private void Update()
	{
		if(cvc.Follow == null)
		{
			Destroy(gameObject);
		}
	}
}
