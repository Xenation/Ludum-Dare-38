using UnityEngine;

namespace Assets.Scripts {
	public class Bullet : Entity {

		public float lifeTime = 5;
		public int damage = 1;
		public int bounceCount = 0;

		private float startTime;
		private TrailRenderer trail;

		public override void Start() {
			base.Start();
			startTime = Time.time;
			trail = GetComponentInChildren<TrailRenderer>();
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
				actor.audioSrc.PlayOneShot(SoundManager.Instance.GetHitSound());
			}
			if (bounceCount <= 0) {
				if (trail != null) {
					trail.transform.parent = null;
					trail.autodestruct = true;
				}
				Destroy(gameObject);
				CallOnDestroy();
			}
			bounceCount--;
		}

	}
}
