public class ShotgunPellet : BaseBullet{
	private float speedDrag = 50f;
	private float stoppingFactor = 10f;
	private float penetrationFactor = .4f;

	public const float baseSpeed = 120f;
	public const int baseDamage = 10;

	public override float getBaseSpeed() {
		return baseSpeed;
	}

	public override int getBaseDamage() {
		return baseDamage;
	}

	public override float getStoppingFactor() {
		return stoppingFactor;
	}

	public override float getSpeedDrag() {
		return speedDrag;
	}

	public override float getPenetrationFactor() {
		return penetrationFactor;
	}
}