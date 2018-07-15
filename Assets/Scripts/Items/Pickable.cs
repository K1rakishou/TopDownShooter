using UnityEngine;

namespace Items{

	public abstract class Pickable : MonoBehaviour{
		private int maxLifeTime = 5;
		
		private void Start() {
			Destroy(gameObject, maxLifeTime);
		}

		private void OnTriggerEnter(Collider other) {
			if (other.gameObject.CompareTag("Player")) {
				var player = other.gameObject.GetComponent<PlayerController>();
				if (player != null) {
					apply(player);
					Destroy(gameObject);
				}
			}
		}

		protected abstract void apply(PlayerController player);

	}

}