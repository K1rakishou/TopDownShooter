using System.Collections;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class Shotgun : BaseWeapon{
	private float spreadFactor = 100;
	private float weaponSpread;

	public ShotgunPellet bullet;
	public int innerCirclePelletsCount = 3;
	public int outerCirclePelletsCount = 15;
	public float timeBetweenShots = .8f;
	public Transform firePoint;

	public override void startShooting() {
		if (!isFiring) {
			StartCoroutine(shoot());
		}
	}

	public override void stopShooting() {
	}

	protected override IEnumerator shoot() {
		isFiring = true;
		spawnBullets();
		yield return new WaitForSeconds(timeBetweenShots);
		isFiring = false;
	}

	private void spawnBullets() {
		for (var i = 0; i < innerCirclePelletsCount; ++i) {
			spawnBullet(0.05f);
		}

		for (var i = 0; i < outerCirclePelletsCount; ++i) {
			spawnBullet(0.1f);
		}
	}

	private void spawnBullet(float minSpread) {
		weaponSpread = (player.getPlayerSpeed() / spreadFactor) + minSpread;
		var rotation = firePoint.rotation;
		rotation.y += Random.Range(-weaponSpread, weaponSpread);

		Instantiate(bullet, firePoint.position, rotation);
	}
	
	public override WeaponType getWeaponType() {
		return WeaponType.SemiAutomatic;
	}

	public override float getWeaponSpread() {
		return weaponSpread;
	}
}