using System;
using UnityEngine;

public class OnGround : MonoBehaviour {
	[SerializeField] private String[] tags;

	public bool isOnGround;
	private void OnTriggerStay(Collider other) {
		foreach(var compareTag in tags) {
			if(other.CompareTag(compareTag)) {
				isOnGround = true;
				return;
			}
		}

		isOnGround = false;
	}

	private void OnTriggerExit(Collider other) {
		isOnGround = false;
	}
}
