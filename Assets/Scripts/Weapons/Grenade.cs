using UnityEngine;

namespace Weapons{

	public class Grenade : BaseWeapon{
		private float weaponSpread = 0f;


		public Vector3 startPosition;
		public Vector3 endPosition;
		public float throwStrength = 1f;
		public float gravity = 9.81f;


		// protected override void Update() {
		// 	base.Update();
		// 	
		// 	var destination = player.getPointToLook();
		// 	var position = player.getPlayerPosition();
		// 	var nextPos = position;
		// 	var stepSize = 1f / 100f;
		// 	
		// 	for (var step = 0f; step < 1f; step += stepSize) {
		// 		nextPos += throwStrength * (Physics.gravity + position) * Time.deltaTime;
		// 		Debug.DrawLine(position, nextPos, Color.green);
		// 	}
		// }

		private void OnDrawGizmos() {
			// var position = startPosition;
			// var nextPos = position;
			var stepSize = 1f / 100f;

			// var startPosition = Vector3.zero;
			// var throwVector = ((Vector3.forward + Vector3.up) / 2f).normalized;

			var delta = (endPosition - startPosition) * stepSize;
			var position1 = startPosition;

			for (var step = 0f; step < 1f; step += stepSize) {
				var nextPosition = position1 - Physics.gravity + delta * throwStrength * step;
				
				Gizmos.color = Random.ColorHSV();
				Gizmos.DrawLine(position1, nextPosition);
				position1 = nextPosition;
			}

			// for (var step = 0f; step < 1f; step += stepSize) {
			// 	nextPos += throwStrength * (Physics.gravity + throwVector) * Time.deltaTime;
			// 	Gizmos.DrawLine(position, nextPos);
			// 	position = nextPos;
			// }
		}

		public override void fireButtonPressed() {
			throwStrength += 1f * Time.deltaTime;
		}

		public override void fireButtonReleased() {
			throwStrength -= 1f * Time.deltaTime;
		}

		public override WeaponShootType getWeaponShootType() {
			return WeaponShootType.SemiAutomatic;
		}

		public override Weapon getWeaponType() {
			return Weapon.Grenade;
		}

		public override float getWeaponSpread() {
			return weaponSpread;
		}
	}

}