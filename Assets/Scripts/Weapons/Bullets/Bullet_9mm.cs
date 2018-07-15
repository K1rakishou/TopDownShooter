namespace Weapons.Bullets{

	public class Bullet_9mm : BaseBullet{
		private float speedDrag = 100f;
		private float stoppingFactor = 200f;
		private float pushBackFactor = 300f;
		private float penetrationFactor = .2f;

		public const float baseSpeed = 110f;
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

		public override float getPushbackFactor() {
			return pushBackFactor;
		}

		public override float getSpeedDrag() {
			return speedDrag;
		}

		public override float getPenetrationFactor() {
			return penetrationFactor;
		}
	}

}