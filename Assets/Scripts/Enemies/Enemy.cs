using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour{
	private Rigidbody myRigidbody;
	private PlayerController player;
	private int currentHealth;
	private float currentSpeed;
	private bool isCollectingDamage = false;

	public HurtZone hurtZone;
	public Image healthBar;
	public int maxHealth = 100;
	public float maxSpeed = 5f;

	void Awake() {
		currentHealth = maxHealth;
		currentSpeed = maxSpeed;
		
		myRigidbody = GetComponent<Rigidbody>();
		player = FindObjectOfType<PlayerController>();
	}

	void FixedUpdate() {
		var playerPositionMaybe = player.getPlayerPosition();
		if (!playerPositionMaybe.HasValue) {
			return;
		}

		currentSpeed += .05f;
		if (currentSpeed > maxSpeed) {
			currentSpeed = maxSpeed;
		}

		if (currentSpeed < 0f) {
			currentSpeed = 0f;
		}

		transform.LookAt(player.transform.position);
		myRigidbody.velocity = transform.forward * currentSpeed;
	}
	
	void Update() {
		if (player == null) {
			return;
		}

		if (currentHealth <= 0) {
			Destroy(gameObject);
		}
	}

	public void hurtEnemy(BaseBullet bullet, int damage) {
		applyDamage(bullet, damage);
	}

	private void applyDamage(BaseBullet bullet, int damage) {
		var playerPositionMaybe = player.getPlayerPosition();
		if (!playerPositionMaybe.HasValue) {
			return;
		}

		var playerPosition = (Vector3) playerPositionMaybe;
		var stoppingFactor = bullet.getStoppingFactor();
		var enemyPosition = transform.position;
		var distanceVector = playerPosition - enemyPosition;
		var distanceVectorNormalized = distanceVector.normalized;
		var resultVector = distanceVectorNormalized * stoppingFactor;

		currentHealth -= damage;
		currentSpeed -= maxSpeed;
		if (currentSpeed < 1f) {
			currentSpeed = 1f;
		}

		healthBar.fillAmount = currentHealth / (float) maxHealth;
		myRigidbody.AddForce(-resultVector, ForceMode.Force);
	}

	public void slowDown() {
		currentSpeed -= ((maxSpeed / 100) * 90);
	}
}