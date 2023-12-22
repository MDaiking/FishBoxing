using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private bool isCursorShow;
    public bool IsCursorShow
	{
        get{ return isCursorShow; }
        set{ isCursorShow = value; }
	}
    private void Start()
    {
        isCursorShow = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) isCursorShow = !isCursorShow;
		if (isCursorShow)
		{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
		else
		{
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
