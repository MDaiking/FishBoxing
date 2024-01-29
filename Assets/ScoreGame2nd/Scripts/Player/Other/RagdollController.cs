using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(PlayerMove))]
public class RagdollController : MonoBehaviour
{
	PlayerMove playerMove;
	Animator animator;
	[SerializeField]
	Rigidbody[] rigidbodies;
	[SerializeField]
	Collider[] colliders;
	private void Start()
	{
		playerMove = GetComponent<PlayerMove>();
		rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
		colliders = gameObject.GetComponentsInChildren<Collider>();
		bool isFirst = true;
		foreach (Rigidbody rb in rigidbodies)
		{
			if (rb == null) continue;
			if (isFirst)
			{
				isFirst = false;
				continue;
			}
			rb.mass = 0.5f;
			rb.isKinematic = true;
		}
		isFirst = true;
		foreach (Collider collider in colliders)
		{
			if (collider == null) continue;
			if (isFirst)
			{
				isFirst = false;
				continue;
			}
			collider.isTrigger = true;
		}
		animator = GetComponent<Animator>();
		SetRagdoll(false, false);
	}

	public void SetRagdoll(bool isEnabled, bool isInWater)
	{
		animator.enabled = !isEnabled;
		if (isEnabled)
		{
			bool isFirst = true;
			foreach (Collider collider in colliders)
			{
				if (collider == null) continue;
				if (isFirst)
				{
					isFirst = false;
					continue;
				}

				collider.isTrigger = false;

			}
			isFirst = true;
			if (isInWater)
			{
				foreach (Rigidbody rb in rigidbodies)
				{
					if (rb == null) continue;
					if (isFirst)
					{
						isFirst = false;
						continue;
					}
					rb.isKinematic = false;
					rb.drag = 4.0f;
				}
			}
			else
			{
				foreach (Rigidbody rb in rigidbodies)
				{
					if (rb == null) continue;
					if (isFirst)
					{
						isFirst = false;
						continue;
					}
					rb.isKinematic = false;
					rb.drag = 1.0f;
				}
			}
		}
	}
}
