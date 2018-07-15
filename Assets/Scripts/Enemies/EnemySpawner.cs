using System.Collections;
using UnityEngine;

namespace Enemies{

	public class EnemySpawner : MonoBehaviour{
		public PlayerController player;
		public Enemy enemy;

		void Start() {
			StartCoroutine(spawnTimer());
		}

		IEnumerator spawnTimer() {
			for (var i = 0; i < 100; ++i) {
				yield return new WaitForSeconds(5);
				spawnEmeny();
			}
		}

		void spawnEmeny() {
			var playerPosition = player.getPlayerPosition();
			var randomPoint = Random.insideUnitCircle;
			var spawnPosition = new Vector3(randomPoint.x + playerPosition.x, 0f, randomPoint.y + playerPosition.z);
			spawnPosition += (playerPosition - spawnPosition).normalized * 50f;
			spawnPosition.y = 0f;
		
			Instantiate(enemy, spawnPosition, Quaternion.identity);
		}
	}

}