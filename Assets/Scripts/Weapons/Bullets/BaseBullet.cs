using UnityEngine;

public abstract class BaseBullet : MonoBehaviour{
	protected PlayerController player;
	protected Rigidbody myRigidbody;
	protected float currentSpeed;
	protected float minBulletSpeed;

	protected virtual void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		myRigidbody = GetComponent<Rigidbody>();

		var speed = player.getPlayerMaxSpeed() + this.getBaseSpeed();
		var maxDeltaSpeed = speed / 20f;       //5 perceents of the speed
		minBulletSpeed = speed - (speed / 5f); //baseSpeed - 20% of it 
		currentSpeed = speed + Random.Range(-maxDeltaSpeed, maxDeltaSpeed);
	}

	protected virtual void FixedUpdate() {
		myRigidbody.AddForce(transform.forward * currentSpeed * 25f);
	}

	protected virtual void Update() {
		 if (currentSpeed < minBulletSpeed) {
		 	Destroy(gameObject);
		 	return;
		 }
		
		 currentSpeed -= this.getSpeedDrag() * Time.deltaTime;
	}

	protected virtual void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().hurtEnemy(this, this.getBaseDamage());
		}

		Destroy(gameObject);
	}

	public abstract float getBaseSpeed();
	public abstract int getBaseDamage();
	public abstract float getCurrentSpeed();
	public abstract float getStoppingFactor();
	public abstract float getSpeedDrag();
}