using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]

//��]�̋���������e�X�g�p�X�N���v�g
public class RotateText : MonoBehaviour
{
	private float rotate;
	public float Rotate
	{
		get
		{
			return rotate;
		}
		set
		{
			rotate = value;
		}
	}
	private Text text;
	private void Start()
	{
		text = GetComponent<Text>();
	}
	private void Update()
	{
		text.text = "" + rotate;
	}
}
