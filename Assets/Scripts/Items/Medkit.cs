using Items;

public class Medkit : Pickable {

	protected override void apply(PlayerController player) {
		player.heal(50);
	}
}
