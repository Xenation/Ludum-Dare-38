using UnityEngine;

namespace Assets.Scripts {
	public class Character : Actor {

		public GameObject arm;

		protected SpriteRenderer sprite;
		protected Vector3 targetDir;

		public override void Start() {
			base.Start();
			sprite = GetComponent<SpriteRenderer>();
		}

		public override void Update() {
			base.Update();
			UpdateArm();
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
