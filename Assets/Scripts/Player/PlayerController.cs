using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class PlayerController : MonoBehaviour{
	private Rigidbody myRigidBody;
	private Vector3 velocity;
	private Camera mainCamera;
	private int currentHealth;
	private float currentSpeed;

	public Image healthBar;
	public int maxHealth = 100;
	public float maxSpeed = 5f;
	public float aimingSpeed = 2f;
	public BaseWeapon currentWeapon;

	void Start() {
		currentHealth = maxHealth;
		currentSpeed = 0f;
		myRigidBody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}

	void FixedUpdate() {
		myRigidBody.velocity = velocity;
	}

	void Update() {
		if (currentHealth <= 0) {
			Destroy(gameObject);
			return;
		}

		float currentMaxSpeed = maxSpeed;
		if (Input.GetMouseButton(1)) {
			currentMaxSpeed = aimingSpeed;
		} else {
			currentMaxSpeed = maxSpeed;
		}
		
		var moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		
		handleMouseInput();

		velocity = updatePlayerVelocity(moveInput, currentMaxSpeed);
	}

	private Vector3 updatePlayerVelocity(Vector3 moveInput, float _maxSpeed) {
		currentSpeed += _maxSpeed * Time.deltaTime;
		if (currentSpeed <= 0f) {
			currentSpeed = 0f;
		}

		if (currentSpeed >= _maxSpeed) {
			currentSpeed = _maxSpeed;
		}
		
		return moveInput * currentSpeed;
	}

	private void handleMouseInput() {
		var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		var groundPlane = new Plane(Vector3.up, Vector3.zero);

		if (groundPlane.Raycast(cameraRay, out var rayLength)) {
			var pointToLook = cameraRay.GetPoint(rayLength);
			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}

		if (currentWeapon.getWeaponType() == BaseWeapon.WeaponType.SemiAutomatic) {
			if (Input.GetMouseButtonDown(0)) {
				currentWeapon.startShooting();
			}

			if (Input.GetMouseButtonUp(0)) {
				currentWeapon.stopShooting();
			}
		} else if (currentWeapon.getWeaponType() == BaseWeapon.WeaponType.Automatic) {
			if (Input.GetMouseButton(0)) {
				currentWeapon.startShooting();
			} else {
				currentWeapon.stopShooting();
			}
		}
	}

	public void takeDamage(int damage) {
		currentHealth -= damage;
		healthBar.fillAmount = (float) currentHealth / (float) maxHealth;
	}
	
	public float getPlayerMaxSpeed() {
		return maxSpeed;
	}
	
	public float getPlayerSpeed() {
		return myRigidBody.velocity.magnitude;
	}

	public Vector3? getPlayerPosition() {
		if (myRigidBody == null) {
			return null;
		}

		return myRigidBody.position;
	}
}