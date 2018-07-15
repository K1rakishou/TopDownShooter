using UnityEngine;
using Weapons;

namespace Items{

	public class WeaponBox : Pickable{
		public BaseWeapon[] weapons;
		
		protected override void apply(PlayerController player) {
			var weaponToInstantiate = weapons[Random.Range(0, weapons.Length)];
			player.replaceWeapon(weaponToInstantiate);
			
			Debug.Log("Weapon replaced");
		}
	}

}