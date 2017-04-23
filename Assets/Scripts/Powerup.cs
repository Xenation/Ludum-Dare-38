using UnityEngine;

namespace Assets.Scripts {
	public class Powerup : Entity {

		public Powerup() {
			UseGravity = false;
		}

		public PowerupEffect effect;

		public void OnTriggerEnter2D(Collider2D col) {
			Player player = col.gameObject.GetComponent<Player>();
			if (player == null) return;
			player.ApplyPowerupEffect(effect);
			Destroy(gameObject);
			CallOnDestroy();
		}

	}
}
