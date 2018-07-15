using System.Collections;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class AssaultRifle : BaseWeapon{
	private const float minSpread = .005f;
	private const float maxSpread = .06f;
	private const float addDeltaSpread = maxSpread * 2;
	private const float subDeltaSpread = maxSpread * 3;
	private const float shotsPerMinute = 750f;
	private const float oneMinute = 60f;
	private const float fireRate = shotsPerMinute / oneMinute;
	private float timeBetweenShots;
	private float weaponSpread = minSpread;

	public Bullet_5x45 bullet;
	public Transform firePoint;

	protected override void Start() {
		timeBetweenShots = 1f / fireRate;

		base.Start();
	}

	protected override void Update() {
		updateSpread();
		base.Update();
	}

	public override void startShooting() {
		if (!isFiring) {
			StartCoroutine(shoot());
		}
	}

	public override void stopShooting() {
	}

	private void updateSpread() {
		if (isFiring) {
			weaponSpread += addDeltaSpread * Time.deltaTime;
			if (weaponSpread > maxSpread) {
				weaponSpread = maxSpread;
			}
		} else {
			weaponSpread -= subDeltaSpread * Time.deltaTime;
			if (weaponSpread < minSpread) {
				weaponSpread = minSpread;
			}
		}
	}

	private IEnumerator shoot() {
		isFiring = true;
		spawnBullet();
		yield return new WaitForSeconds(timeBetweenShots);
		isFiring = false;
	}

	private void spawnBullet() {
		var newWeaponSpread = weaponSpread;
		var playerSpeed = player.getPlayerSpeed();
		
		if (playerSpeed <= .1f) {
			newWeaponSpread /= 5f;
		}
		
		if (playerSpeed > .1f && playerSpeed <= player.getPlayerAimingSpeed()) {
			newWeaponSpread /= 3f;
		}
		
		var rotation = firePoint.rotation;
		rotation.y += Random.Range(-newWeaponSpread, newWeaponSpread);
		
		Instantiate(bullet, firePoint.position, rotation);
	}

	public override WeaponShootType getWeaponShootType() {
		return WeaponShootType.Automatic;
	}

	public override Weapon getWeaponType() {
		return Weapon.AssaultRifle;
	}

	public override float getWeaponSpread() {
		return weaponSpread;
	}
}