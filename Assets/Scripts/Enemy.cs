using UnityEngine;

namespace Assets.Scripts {
	public class Enemy : Actor {

		public float viewDistance = 30;
		public float minDistance = 5;

		public override void Start() {
			base.Start();
		}

		public override void Update() {
			base.Update();
			if (ActorManager.Instance.player.isAlive && SeesPlayer()) {
				if (ToPlayer().magnitude > minDistance) {
					if (IsPlayerAtRight()) {
						horizontalVel = speed;
					} else {
						horizontalVel = -speed;
					}
				}
				Shoot(ToPlayer().normalized);
			}
		}

		private bool IsPlayerAtRight() {
			return Vector2.Dot(right, ToPlayer()) > 0;
		}

		private bool SeesPlayer() {
			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, ToPlayer(), viewDistance);
			for (int i = 0; i < hits.Length; i++) {
				if (hits[i].transform == transform) continue;
				else if (hits[i].transform == ActorManager.Instance.player.transform) {
					return true;
				} else {
					return false;
				}
			}
			return false;
		}

		private Vector2 ToPlayer() {
			return ActorManager.Instance.player.transform.position - transform.position;
		}

	}
}
