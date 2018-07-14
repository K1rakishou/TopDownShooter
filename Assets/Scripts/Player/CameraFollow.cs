using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
	public Transform target;

	void Start() {
	}

	void FixedUpdate() {
		if (target == null) {
			return;
		}

		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - 3.0f);
	}
}