using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour {
	[Header("Attributes")]
	public PieceType Type;
	public Vector2Int CurrentGridPos;

	private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	public void Destroy() {
		Destroy(gameObject);
	}

	public void Cleared() {
		// Destory piece after blinking animation is over.
		anim.SetTrigger("Death");
		Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
	}
}
