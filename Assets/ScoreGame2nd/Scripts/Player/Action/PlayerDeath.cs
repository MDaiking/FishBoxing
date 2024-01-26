using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RagdollController))]

public class PlayerDeath : MonoBehaviour
{
	private Animator animator;
	private RagdollController ragdoll;
	private void Start()
	{
		animator = GetComponent<Animator>();
		ragdoll = GetComponent<RagdollController>();
	}
	public void Death()
	{
		ragdoll.SetRagdoll(true);
	}
}
