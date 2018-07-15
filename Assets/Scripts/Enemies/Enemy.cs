using System.Collections;
using DefaultNamespace.Items;
using UnityEngine;
using UnityEngine.UI;

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
	private float chanceToDropItem = .1f;

	public Pickable[] items;
	public Image healthBar;
	public int maxHealth = 100;
	public float maxSpeed = 5f;
	public float acceleration = 5f;

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
		if (Random.Range(0f, 1f) <= chanceToDropItem) {
			spawnItem();
		}

		Destroy(gameObject);
	}

	private void spawnItem() {
		Instantiate(items[0], transform.position, transform.rotation);
	}

	public void hurtEnemy(BaseBullet bullet) {
		takeDamage(bullet, bullet.getBaseDamage());
	}

	private void takeDamage(BaseBullet bullet, int damage) {
		var playerPosition = player.getPlayerPosition();
		var stoppingFactor = bullet.getStoppingFactor();
		var enemyPosition = transform.position;
		var distanceVector = playerPosition - enemyPosition;
		var distanceVectorNormalized = distanceVector.normalized;
		var resultVector = distanceVectorNormalized * stoppingFactor;
		
		slowDown(10f);
		updateHealth(damage);
		
		myRigidbody.AddForce(-resultVector, ForceMode.Force);
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