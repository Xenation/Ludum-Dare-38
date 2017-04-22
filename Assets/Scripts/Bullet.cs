using UnityEngine;

namespace Assets.Scripts {
	public class Bullet : Entity {

		public float lifeTime = 5;
		public int damage = 1;

		private float startTime;

		public override void Start() {
			base.Start();
			startTime = Time.time;
			
		}

		public override void Update() {
			base.Update();
			//Debug.Log(Time.time);
			if (Time.time - startTime > lifeTime) {
				Destroy(gameObject);
				CallOnDestroy();
			}
		}

		public void OnCollisionEnter2D(Collision2D col) {
			//Debug.Log("collision");
			Actor actor = col.collider.GetComponent<Actor>();
			if (actor != null) {
				actor.ApplyDamage(damage);
			}
			Destroy(gameObject);
			CallOnDestroy();
		}

	}
}
