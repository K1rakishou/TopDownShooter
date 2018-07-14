using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour{
	private bool isInflictingDamage;
	private float damageTimeout = 1f;

	public Enemy enemy;

	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			if (!isInflictingDamage) {
				isInflictingDamage = true;
				StartCoroutine(hurtPlayer(other));
			}
		}
	}

	private IEnumerator hurtPlayer(Collider other) {
		other.gameObject.GetComponent<PlayerController>().takeDamage(10);
		enemy.slowDown();
		
		yield return new WaitForSeconds(damageTimeout);
		isInflictingDamage = false;
	}

	public void setDamageTimeout(float _damageTimeout) {
		damageTimeout = _damageTimeout;
	}
}