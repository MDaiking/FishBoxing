using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerKnockback),typeof(Rigidbody))]

public class DecoyMove : MonoBehaviour
{
    private PlayerKnockback playerKnockback;
    private Rigidbody rb;
    private Vector3 defaultPos;
    private void Start()
    {
        playerKnockback = GetComponent<PlayerKnockback>();
        rb = GetComponent<Rigidbody>();
        defaultPos = transform.position;
    }
    private void Update()
    {
        rb.velocity = playerKnockback.CalcKnockback() + Gravity();
        if(transform.position.y <= 0.0f)
		{
            transform.position = defaultPos;
		}
    }
    private Vector3 Gravity()
	{
        return new Vector3(0.0f,rb.velocity.y,0.0f);
	}
}
