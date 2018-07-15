using UnityEngine;
using Weapons;

namespace Items{

	public class WeaponBox : Pickable{
		public BaseWeapon[] weapons;
		
		protected override void apply(PlayerController player) {
			BaseWeapon weaponToInstantiate;
			
			while (true) {
				weaponToInstantiate = weapons[Random.Range(0, weapons.Length)];
				if (weaponToInstantiate.getWeaponType() != player.getCurrentWeapon().getWeaponType()) {
					break;
				}
			}
			
			player.replaceWeapon(weaponToInstantiate);
			Debug.Log("Weapon replaced");
		}
	}

}