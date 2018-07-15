using System.Collections;
using System.Linq;
using Items;
using UnityEngine;
using UnityEngine.UI;
using Weapons;
using Weapons.Projectiles;

namespace Enemies{

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(PlayerController))]
	public class Enemy : MonoBehaviour{
		private Rigidbody myRigidbody;
		private PlayerController player;
		private int currentHealth;
		private float currentSpeed;
		private bool isInflictingDamage;
		private int damageTimeout = 1;
		private float minDistanceToStartHurtingPlayer = 1.75f;

		public Pickable[] items;
		public Image healthBar;
		public int maxHealth = 100;
		public float maxSpeed = 5f;
		public float acceleration = 5f;
		public float chanceToDropItem = .1f;

		void Awake() {
			currentHealth = maxHealth;
			currentSpeed = maxSpeed;
			isInflictingDamage = false;

			myRigidbody = GetComponent<Rigidbody>();
			player = FindObjectOfType<PlayerController>();
		}

		void FixedUpdate() {
			myRigidbody.velocity = transform.forward * currentSpeed;
		}

		void Update() {
			if (player == null) {
				return;
			}

			if (currentHealth <= 0) {
				die();
				return;
			}

			updateCurrentSpeed();

			if ((transform.position - player.getPlayerPosition()).magnitude < minDistanceToStartHurtingPlayer) {
				tryToHurtPlayer();
			}
		}

		private void tryToHurtPlayer() {
			if (!isInflictingDamage) {
				isInflictingDamage = true;
				StartCoroutine(hurtPlayer());
			}
		}

		private IEnumerator hurtPlayer() {
			player.takeDamage(10);
			slowDown(90f);

			yield return new WaitForSeconds(damageTimeout);
			isInflictingDamage = false;
		}

		private void updateCurrentSpeed() {
			currentSpeed += acceleration * Time.deltaTime;
			if (currentSpeed > maxSpeed) {
				currentSpeed = maxSpeed;
			}

			if (currentSpeed < 0f) {
				currentSpeed = 0f;
			}

			transform.LookAt(player.transform.position);
		}

		private void die() {
			tryToSpawnItem();
			Destroy(gameObject);
		}

		private void tryToSpawnItem() {
			Pickable itemToInstantiate = null;
		
			if (player.currentWeapon.getWeaponType() == BaseWeapon.Weapon.P250) {
				var weaponDropChance = Random.Range(0, 3);
				if (weaponDropChance == 0) {
					var weaponBox = items.First(item => item is WeaponBox) as WeaponBox;
					if (weaponBox == null) {
						Debug.LogError("WeaponBox is NULL!!!");
						return;
					}
			
					itemToInstantiate = weaponBox;
				}
			} else {
				var itemDropChance = Random.Range(0f, 1f);
				if (itemDropChance <= chanceToDropItem) {
					itemToInstantiate = items[Random.Range(0, items.Length)];
				}
			}

			if (itemToInstantiate != null) {
				Instantiate(itemToInstantiate, transform.position, transform.rotation);
			}
		}

		public void hurtEnemy(BaseProjectile projectile) {
			takeDamage(projectile, projectile.getBaseDamage());
		}

		private void takeDamage(BaseProjectile projectile, int damage) {
			var playerPosition = player.getPlayerPosition();
			var pushbackFactor = projectile.getPushbackFactor();
			var enemyPosition = transform.position;
			var distanceVector = playerPosition - enemyPosition;
			var distanceVectorNormalized = distanceVector.normalized;
			var resultVector = distanceVectorNormalized * pushbackFactor;
			myRigidbody.AddForce(-resultVector, ForceMode.Force);
			
			var percent = 100f / (projectile.getMaxStoppingFactor() / projectile.getStoppingFactor());
			slowDown(percent);
			updateHealth(damage);
		}

		private void updateHealth(int damage) {
			currentHealth -= damage;
			healthBar.fillAmount = currentHealth / (float) maxHealth;
		}

		public void slowDown(float percent) {
			currentSpeed -= ((maxSpeed / 100f) * percent);
			
			if (currentSpeed < 1f) {
				currentSpeed = 1f;
			}
		}
	}

}