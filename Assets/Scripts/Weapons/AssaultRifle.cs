using System.Collections;
using UnityEngine;
using Weapons;

public class AssaultRifle : BaseWeapon{
	private float spreadFactor = 100;
	private float weaponSpread;

	public Bullet_5x45 bullet;
	public float timeBetweenShots = .1f;
	public Transform firePoint;

	public override void startShooting() {
		if (!isFiring) {
			StartCoroutine(shoot());
		}
	}
	
	public override void stopShooting() {
		//do nothing
	}

	protected override IEnumerator shoot() {
		isFiring = true;
		spawnBullet();
		yield return new WaitForSeconds(timeBetweenShots);
		isFiring = false;
	}

	private void spawnBullet() {
		weaponSpread = player.getPlayerSpeed() / spreadFactor;
		var rotation = firePoint.rotation;
		rotation.y += Random.Range(-weaponSpread, weaponSpread);
		
		Instantiate(bullet, firePoint.position, rotation);
	}
	
	public override WeaponType getWeaponType() {
		return WeaponType.Automatic;
	}

	public override float getWeaponSpread() {
		return weaponSpread;
	}
}