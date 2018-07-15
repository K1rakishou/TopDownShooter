using System.Collections;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons{

	public class P250 : BaseWeapon{
		private const float minSpread = .005f;
		private const float maxSpread = .05f;
		private float weaponSpread = minSpread;
		
		public Bullet_9mm bullet;
		public Transform firePoint;
		private float timeBetweenShots = .1f;

		private void Update() {
			updateSpread();
			base.Update();
		}

		public override void fireButtonPressed() {
			if (!isFiring) {
				StartCoroutine(shoot());
			}
		}

		public override void fireButtonReleased() {
		}
		
		private void updateSpread() {
			weaponSpread -= (minSpread * 3) * Time.deltaTime;
			if (weaponSpread < minSpread) {
				weaponSpread = minSpread;
			}
		}

		private IEnumerator shoot() {
			isFiring = true;
			spawnBullet();
			yield return new WaitForSeconds(timeBetweenShots);
			isFiring = false;
		}

		private void spawnBullet() {
			weaponSpread += minSpread;

			if (weaponSpread > maxSpread) {
				weaponSpread = maxSpread;
			}
			
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
			return WeaponShootType.SemiAutomatic;
		}

		public override Weapon getWeaponType() {
			return Weapon.P250;
		}

		public override float getWeaponSpread() {
			return weaponSpread;
		}
	}

}