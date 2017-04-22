using UnityEngine;

namespace Assets.Scripts {
	public class Actor : Entity {

		public AudioSource audioSrc;
		public GameObject bullet;
		public ParticleSystem shootParticles;
		public AudioClip shootSound;
		public float shootMult = .5f;
		public float shotForce = 30;
		public float shootDelay = .5f;
		public ParticleSystem dieParticles;
		public AudioClip dieSound;
		public int health = 5;
		public float speed = 2f;
		public float jumpPower = 30f;

		public bool isAlive { get; private set; }

		protected float horizontalVel = 0;

		private float lastShotTime = 0;

		public override void Start() {
			base.Start();
			isAlive = true;
		}

		public override void Update() {
			base.Update();
			horizontalVel = 0;
			if (health <= 0) {
				Die();
			}
		}

		public override void FixedUpdate() {
			base.FixedUpdate();
			SetHorizontalRelativeVelocity(horizontalVel);
		}

		public void Shoot(Vector2 direction) {
			if (Time.time - lastShotTime < shootDelay) return;
			lastShotTime = Time.time;
			GameObject bul = Instantiate<GameObject>(bullet, transform.position + (Vector3) direction * shootMult, transform.localRotation, ActorManager.Instance.entitiesParent);
			bul.GetComponent<Rigidbody2D>().AddForce(direction * shotForce, ForceMode2D.Impulse);
			Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			//ParticleSystem syst = Instantiate<ParticleSystem>(shootParticles, transform.position + (Vector3) direction * shootMult, transform.localRotation, ActorManager.Instance.entitiesParent);
			shootParticles.transform.position = transform.position + (Vector3) direction * shootMult;
			shootParticles.transform.LookAt(transform.position + (Vector3) direction * (shootMult + 1));
			shootParticles.Play();
			audioSrc.PlayOneShot(shootSound);
			GravityManager.Instance.RegisterEntity(bul);
		}

		public void ApplyDamage(int damage) {
			health -= damage;
		}

		public void Die() {
			isAlive = false;
			if (dieParticles != null) {
				ParticleSystem dieEffect = Instantiate<ParticleSystem>(dieParticles, transform.position, Quaternion.identity);
				dieEffect.Play();
				ParticleSystem subSyst = dieEffect.GetComponentInChildren<ParticleSystem>();
				if (subSyst != null) {
					subSyst.Play();
				}
			}
			if (dieSound != null) {
				audioSrc.transform.parent = null;
				audioSrc.PlayOneShot(dieSound);
			}
			
			Destroy(gameObject);
			CallOnDestroy();
		}

	}
}
