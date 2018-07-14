public class Bullet_5x45 : BaseBullet{
	private float speedDrag = 3f;
	private float stoppingFactor = 300f;

	public const float baseSpeed = 80f;
	public const int baseDamage = 15;

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