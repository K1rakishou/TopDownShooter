using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies{

	public class SpawnWave{
		public readonly int enemiesInWave;
		public readonly float delayBeforeNextEnemySpawn;
		public readonly float delayBeforeNextWave;

		public SpawnWave(int enemiesInWave, float delayBeforeNextEnemySpawn, float delayBeforeNextWave) {
			this.enemiesInWave = enemiesInWave;
			this.delayBeforeNextEnemySpawn = delayBeforeNextEnemySpawn;
			this.delayBeforeNextWave = delayBeforeNextWave;
		}
	}

	public class EnemySpawner : MonoBehaviour{
		private List<SpawnWave> spawnWaves;
		
		public PlayerController player;
		public Enemy enemy;

		void Start() {
			spawnWaves = new List<SpawnWave> {
				new SpawnWave(10, 3, 10),
				new SpawnWave(25, 2, 10),
				new SpawnWave(50, 1, 10),
				new SpawnWave(100, .7f, 10),
				new SpawnWave(200, .3f, 0)
			};
			
			// StartCoroutine(spawnTimer());
		}

		IEnumerator spawnTimer() {
			for (var currentWave = 0; currentWave < spawnWaves.Count; currentWave++) {
				var spawnWave = spawnWaves[currentWave];
				Debug.Log($"Wave #{currentWave}, enemiesCount = {spawnWave.enemiesInWave}");

				for (var currentEnemy = 0; currentEnemy < spawnWave.enemiesInWave; currentEnemy++) {
					Debug.Log($"Spawning enemy #{currentEnemy} out of {spawnWave.enemiesInWave}");
					spawnEmeny();
					yield return new WaitForSeconds(spawnWave.delayBeforeNextEnemySpawn);
				}
				
				Debug.Log($"End of wave, next in {spawnWave.delayBeforeNextWave} seconds");
				yield return new WaitForSeconds(spawnWave.delayBeforeNextWave);
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