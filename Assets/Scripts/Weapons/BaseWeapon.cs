using System.Collections;
using UnityEngine;

namespace Weapons{

	public abstract class BaseWeapon : MonoBehaviour{
		protected bool isFiring;
		protected PlayerController player;

		protected void Start() {
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		}

		public virtual void startShooting() {
		}

		public virtual void stopShooting() {
		}

		public abstract WeaponType getWeaponType();
		public abstract float getWeaponSpread();
		protected abstract IEnumerator shoot();

		public enum WeaponType{
			Automatic,
			SemiAutomatic
		}
	}

}