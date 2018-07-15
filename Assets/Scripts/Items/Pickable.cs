using UnityEngine;

namespace DefaultNamespace.Items{

	public abstract class Pickable : MonoBehaviour{

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