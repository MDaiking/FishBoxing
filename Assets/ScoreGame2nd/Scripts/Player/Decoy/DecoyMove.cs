using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerKnockback),typeof(Rigidbody))]

public class DecoyMove : MonoBehaviour
{
    private PlayerKnockback playerKnockback;
    private Rigidbody rb;
    private void Start()
    {
        playerKnockback = GetComponent<PlayerKnockback>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.velocity = playerKnockback.CalcKnockback();
    }
}
