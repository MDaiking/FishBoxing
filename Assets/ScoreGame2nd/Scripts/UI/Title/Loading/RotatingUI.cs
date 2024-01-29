using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RotatingUI : MonoBehaviour
{
	private bool isRotating;
	[SerializeField]
	private float rotateSpeed;
	private RectTransform rTransform;
	public bool IsRotating
	{
		get { return isRotating; }
		set { isRotating = value; }
	}
	private void Start()
	{
		isRotating = true;
		rTransform = GetComponent<RectTransform>();
	}
	private void Update()
	{
		if (isRotating)
		{
			Quaternion rotate = Quaternion.AngleAxis(rotateSpeed, Vector3.forward);
			rTransform.rotation *= rotate;
		}
	}
}
