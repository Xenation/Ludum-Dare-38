using UnityEngine;

namespace Assets.Scripts {
	public class Enemy : Actor {

		public float viewDistance = 30;
		public float minDistance = 5;
		public float jumpDelay = .75f;

		private float lastJumpTime;

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
				if (CheckObstacle()) {
					if (Time.time - lastJumpTime > jumpDelay) {
						Jump();
						lastJumpTime = Time.time;
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

		private bool CheckObstacle() {
			if (horizontalVel < 0) {
				Debug.DrawRay(body.position - localUp * .5f - right2 * .7f, -right2, Color.red);
				RaycastHit2D hit = Physics2D.Raycast(body.position - localUp * .5f - right2, -right2, .025f);
				if (hit) {
					return true;
				}
			} else {
				Debug.DrawRay(body.position - localUp * .5f + right2 * .7f, right2, Color.red);
				RaycastHit2D hit = Physics2D.Raycast(body.position - localUp * .5f + right2, right2, .025f);
				if (hit) {
					return true;
				}
			}
			return false;
		}

	}
}
