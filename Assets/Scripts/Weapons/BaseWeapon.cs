using System.Collections;
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
		
		protected virtual void OnGUI() {
		}

		public virtual void startShooting() {
		}

		public virtual void stopShooting() {
		}

		public abstract WeaponShootType getWeaponShootType();
		public abstract float getWeaponSpread();

		public enum WeaponShootType{
			Automatic,
			SemiAutomatic
		}
	}

}