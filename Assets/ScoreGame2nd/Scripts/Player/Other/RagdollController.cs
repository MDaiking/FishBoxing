using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class RagdollController : MonoBehaviour
{
	Animator animator;
	Rigidbody[] rigidbodies;
	private void Start()
	{
		foreach (Transform child in transform)
		{
			rigidbodies = child.GetComponentsInChildren<Rigidbody>();
		}
		foreach(Rigidbody rb in rigidbodies)
		{
			rb.mass = 0.0f;
		}
		animator = GetComponent<Animator>();
		SetRagdoll(false);
	}
	public void SetRagdoll(bool isEnabled)
	{
		foreach (Rigidbody rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = !isEnabled;
		}
		animator.enabled = !isEnabled;
	}
}
