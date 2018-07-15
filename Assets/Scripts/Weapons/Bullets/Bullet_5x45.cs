public class Bullet_5x45 : BaseBullet{
	private float speedDrag = 30f;
	private float stoppingFactor = 500f;
	private float penetrationFactor = .6f;

	public const float baseSpeed = 150f;
	public const int baseDamage = 15;

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