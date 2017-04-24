using UnityEngine;

namespace Assets.Scripts {
	public class Character : Actor {

		public GameObject arm;
		public float hurtDuration = .5f;

		protected SpriteRenderer sprite;
		protected Vector3 targetDir;

		private bool isHurt = false;
		private float hurtStart;

		public override void Start() {
			base.Start();
			sprite = GetComponent<SpriteRenderer>();
		}

		public override void Update() {
			base.Update();
			UpdateArm();
			if (isHurt) {
				if ((Time.time - hurtStart) / hurtDuration > 1) {
					isHurt = false;
					return;
				}
				float fade = (Time.time - hurtStart) / hurtDuration;
				Color c = sprite.color;
				c.g = fade;
				c.b = fade;
				sprite.color = c;
				Color ca = arm.GetComponent<SpriteRenderer>().color;
				ca.g = fade;
				ca.b = fade;
				arm.GetComponent<SpriteRenderer>().color = ca;
			}
		}

		public override void ApplyDamage(int damage) {
			base.ApplyDamage(damage);
			isHurt = true;
			hurtStart = Time.time;
			Color c = sprite.color;
			c.g = 0;
			c.b = 0;
			sprite.color = c;
			Color ca = arm.GetComponent<SpriteRenderer>().color;
			ca.g = 0;
			ca.b = 0;
			arm.GetComponent<SpriteRenderer>().color = ca;
		}

		private void UpdateArm() {
			if (arm == null) return;
			float angle = Vector2.Angle(Vector2.right, targetDir);
			if (Vector3.Cross(Vector2.right, targetDir).z < 0) {
				angle = 360 - angle;
			}
			if (arm.GetComponent<SpriteRenderer>().flipX) angle -= 180;
			arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

	}
}
