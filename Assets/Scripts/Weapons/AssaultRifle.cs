using System.Collections;
using UnityEngine;
using Weapons;

public class AssaultRifle : BaseWeapon{
	private const float minSpread = .001f;
	private const float maxSpread = .01f;
	private const float shotsPerMinute = 750f;
	private const float oneMinute = 60f;
	private const float fireRate = shotsPerMinute / oneMinute;
	private float timeBetweenShots;
	private float spreadFactor = minSpread;
	private float weaponSpread;

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
			spreadFactor += .015f * Time.deltaTime;
			if (spreadFactor > maxSpread) {
				spreadFactor = maxSpread;
			}
		} else {
			spreadFactor -= .06f * Time.deltaTime;
			if (spreadFactor < minSpread) {
				spreadFactor = minSpread;
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
		weaponSpread = player.getPlayerSpeed() * spreadFactor;
		if (weaponSpread < .01f) {
			weaponSpread = .01f;
		}

		var rotation = firePoint.rotation;
		rotation.y += Random.Range(-weaponSpread, weaponSpread);

		Instantiate(bullet, firePoint.position, rotation);
	}

	public override WeaponShootType getWeaponShootType() {
		return WeaponShootType.Automatic;
	}

	public override float getWeaponSpread() {
		return weaponSpread;
	}
}