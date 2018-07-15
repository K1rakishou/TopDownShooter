using System.Diagnostics;
using DefaultNamespace.Items;
using Debug = UnityEngine.Debug;

public class Medkit : Pickable {

	protected override void apply(PlayerController player) {
		player.heal(50);
		Debug.Log("Picked up medkit");
	}
}
