using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class PlayerController : MonoBehaviour{
	private Vector3 cameraZoomedIn = new Vector3(0f, 14f, -3f);
	private Vector3 cameraZoomedOut = new Vector3(0f, 20f, -5);
	private float cameraZoomSpeed = 2f;
	
	private Rigidbody myRigidBody;
	private Vector3 velocity;
	private Vector3 pointToLook;
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
	
		velocity = updatePlayerVelocity();
		handleMouseInput();
	}

	private Vector3 updatePlayerVelocity() {
		float currentMaxSpeed = 0f;
		if (Input.GetMouseButton(1)) {
			currentMaxSpeed = aimingSpeed;
			zoomCamera(true);
		} else {
			currentMaxSpeed = maxSpeed;
			zoomCamera(false);
		}
		
		currentSpeed += currentMaxSpeed * Time.deltaTime;
		if (currentSpeed <= 0f) {
			currentSpeed = 0f;
		}

		if (currentSpeed >= currentMaxSpeed) {
			currentSpeed = currentMaxSpeed;
		}
		
		var moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		return moveInput * currentSpeed;
	}

	private void zoomCamera(bool zoomIn) {
		var newCameraPos = Vector3.Lerp(mainCamera.transform.position, zoomIn ? cameraZoomedIn : cameraZoomedOut, .2f);

		newCameraPos.x = mainCamera.transform.position.x;
		newCameraPos.z = mainCamera.transform.position.z;
		mainCamera.transform.position = newCameraPos;
	}

	private void handleMouseInput() {
		var mousePosition = Input.mousePosition;
		var cameraRay = mainCamera.ScreenPointToRay(mousePosition);
		var groundPlane = new Plane(Vector3.up, Vector3.zero);

		if (groundPlane.Raycast(cameraRay, out var rayLength)) {
			pointToLook = cameraRay.GetPoint(rayLength);
			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}

		if (currentWeapon == null) {
			return;
		}
		
		if (currentWeapon.getWeaponShootType() == BaseWeapon.WeaponShootType.SemiAutomatic) {
			if (Input.GetMouseButtonDown(0)) {
				currentWeapon.fireButtonPressed();
			}

			if (Input.GetMouseButtonUp(0)) {
				currentWeapon.fireButtonReleased();
			}
		} else if (currentWeapon.getWeaponShootType() == BaseWeapon.WeaponShootType.Automatic) {
			if (Input.GetMouseButton(0)) {
				currentWeapon.fireButtonPressed();
			} else {
				currentWeapon.fireButtonReleased();
			}
		}
	}

	public void takeDamage(int damage) {
		currentHealth -= damage;
		updateHealthBar();
	}

	public void heal(int health) {
		currentHealth += health;
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		
		updateHealthBar();
	}

	public void replaceWeapon(BaseWeapon weapon) {
		var newWeapon = Instantiate(weapon);

		newWeapon.transform.parent = currentWeapon.transform.parent;
		newWeapon.transform.position = currentWeapon.transform.position;
		newWeapon.transform.rotation = currentWeapon.transform.rotation;
		Destroy(currentWeapon.gameObject);

		currentWeapon = newWeapon;
	}

	private void updateHealthBar() {
		healthBar.fillAmount = (float) currentHealth / (float) maxHealth;
	}
	
	public float getPlayerMaxSpeed() {
		return maxSpeed;
	}

	public float getPlayerAimingSpeed() {
		return aimingSpeed;
	}
	
	public float getPlayerSpeed() {
		return myRigidBody.velocity.magnitude;
	}

	public Vector3 getPlayerPosition() {
		if (myRigidBody == null) {
			return Vector3.zero;
		}

		return myRigidBody.position;
	}

	public Vector3 getPointToLook() {
		return pointToLook;
	}

	public BaseWeapon getCurrentWeapon() {
		return currentWeapon;
	}
}