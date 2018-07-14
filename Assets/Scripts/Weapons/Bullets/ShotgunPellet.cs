public class ShotgunPellet : BaseBullet{
	private float speedDrag = 10f;
	private float stoppingFactor = 10f;

	public const float baseSpeed = 70f;
	public const int baseDamage = 6;

	public override float getBaseSpeed() {
		return baseSpeed;
	}

	public override int getBaseDamage() {
		return baseDamage;
	}

	public override float getCurrentSpeed() {
		return currentSpeed;
	}

	public override float getStoppingFactor() {
		return stoppingFactor;
	}

	public override float getSpeedDrag() {
		return speedDrag;
	}
}