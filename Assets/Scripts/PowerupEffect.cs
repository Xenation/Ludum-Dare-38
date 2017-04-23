using UnityEngine;

namespace Assets.Scripts {
	public enum PowerupEffect {
		BOUNCE_BULLET,
		FIRE_SPEED,
		DAMAGE
	}

	public static class PowerupEffectExt {

		public static float GetDuration(this PowerupEffect eff) {
			switch (eff) {
				case PowerupEffect.BOUNCE_BULLET:
					return 5;
				case PowerupEffect.FIRE_SPEED:
					return 4;
				case PowerupEffect.DAMAGE:
					return 4;
				default:
					return 0;
			}
		}

		public static void Apply(this PowerupEffect eff, Actor actor) {
			switch (eff) {
				case PowerupEffect.BOUNCE_BULLET:
					actor.bounceCount = 1;
					break;
				case PowerupEffect.FIRE_SPEED:
					actor.shootDelay /= 2;
					break;
				case PowerupEffect.DAMAGE:
					actor.bulletDamage *= 2;
					break;
			}
		}

		public static void Revert(this PowerupEffect eff, Actor actor) {
			switch (eff) {
				case PowerupEffect.BOUNCE_BULLET:
					actor.bounceCount = 0;
					break;
				case PowerupEffect.FIRE_SPEED:
					actor.shootDelay *= 2;
					break;
				case PowerupEffect.DAMAGE:
					actor.bulletDamage /= 2;
					break;
			}
		}

	}
}
