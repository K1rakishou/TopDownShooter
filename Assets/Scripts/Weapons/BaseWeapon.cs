using UnityEngine;

namespace Weapons{

	public abstract class BaseWeapon : MonoBehaviour{
		protected bool isFiring;
		protected PlayerController player;

		protected virtual void Start() {
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		}
		
		protected virtual void Update() {
			
		}
		
		public virtual void startShooting() {
		}

		public virtual void stopShooting() {
		}

		public abstract WeaponShootType getWeaponShootType();
		public abstract Weapon getWeaponType();
		public abstract float getWeaponSpread();

		public enum WeaponShootType{
			Automatic,
			SemiAutomatic
		}

		public enum Weapon{
			P250,
			AssaultRifle,
			Shotgun
		}
	}

}