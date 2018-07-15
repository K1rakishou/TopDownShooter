using Enemies;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour{
	private float maxStoppingFactor = 1000f;
	private Vector3 bulletVelocity;
	
	protected PlayerController player;
	protected float currentSpeed;
	protected float minBulletSpeed;

	public LayerMask layerMask;

	protected virtual void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		var speed = player.getPlayerSpeed() + this.getBaseSpeed();
		minBulletSpeed = speed - (speed / 1.3f);
		currentSpeed = speed;

		bulletVelocity = transform.forward * currentSpeed;
	}

	protected virtual void Update() {
		var deltaSpeed = getSpeedDrag() * Time.deltaTime;
		currentSpeed -= deltaSpeed;
		
		if (currentSpeed < minBulletSpeed) {
			Destroy(gameObject);
			return;
		}

		var newBulletPositionMaybe = handleBulletFly(transform.position, deltaSpeed, 1f, getPenetrationFactor());
		if (!newBulletPositionMaybe.HasValue) {
			return;
		}

		var newBulletPosition = (Vector3) newBulletPositionMaybe;
		transform.position = newBulletPosition;
	}

	private Vector3? handleBulletFly(Vector3 position, float deltaSpeed, float penetrationDampening, float penetrationFactor) {
		var point1 = position;
		var stepSize = 1f / 6f;
		
		bulletVelocity -= bulletVelocity.normalized * deltaSpeed * Time.deltaTime;
		bulletVelocity /= penetrationDampening;
		
		for (var step = 0f; step < 1f; step += stepSize) {
			bulletVelocity += Vector3.one * stepSize * Time.deltaTime;
			Vector3 point2 = point1 + bulletVelocity * stepSize * Time.deltaTime;

			var ray = new Ray(point1, point2 - point1);
			if (Physics.Raycast(ray, out var hitResult, (point2 - point1).magnitude, layerMask)) {
				if (handleHit(hitResult)) {
					if (Random.Range(0f, 1f) > (1f - penetrationFactor)) {
						return handleBulletFly(hitResult.point, deltaSpeed, penetrationDampening * 2, penetrationFactor / 2);
					}
					
					Destroy(gameObject);
					return null;
				}
			}
			
			point1 = point2;
		}
		
		return point1;
	}

	private bool handleHit(RaycastHit hitResult) {
		var enemyComponent = hitResult.collider.gameObject.GetComponent<Enemy>();
		if (enemyComponent == null) {
			Debug.LogWarning($"Enemy has no Enemy component! object name = {hitResult.collider.gameObject.name}");
			return false;
		}

		enemyComponent.hurtEnemy(this);
		return true;
	}

	public float getCurrentSpeed() {
		return currentSpeed;
	}

	public float getMaxStoppingFactor() {
		return maxStoppingFactor;
	}

	public abstract float getBaseSpeed();
	public abstract int getBaseDamage();
	public abstract float getStoppingFactor();
	public abstract float getPushbackFactor();
	public abstract float getSpeedDrag();
	public abstract float getPenetrationFactor();
}