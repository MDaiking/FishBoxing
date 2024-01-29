using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RagdollController))]

public class PlayerDeath : MonoBehaviour
{
	private RagdollController ragdoll;
	private bool isPlayerDead;
	public bool IsPlayerDead
	{
		get { return isPlayerDead; }
	}
	private void Start()
	{
		ragdoll = GetComponent<RagdollController>();
		isPlayerDead = false;
	}
	public void Death()
	{
		if (!isPlayerDead)
		{
			isPlayerDead = true;
			Debug.Log("Player dead!!!");
			ragdoll.SetRagdoll(true, true);
		}
	}
}
