using UnityEngine;

namespace Assets.Scripts {
	public class Actor : Entity {

		public delegate void OnDeathHandler(Actor actor);
		public event OnDeathHandler OnDeath;

		public AudioSource audioSrc;
		public GameObject bullet;
		public ParticleSystem shootParticles;
		public Vector2 shootOffset = new Vector2(.5f, .05f);
		public float shotForce = 30;
		public float shootDelay = .5f;
		public int bounceCount = 0;
		public int bulletDamage = 1;
		public ParticleSystem dieParticles;
		protected AudioClip dieSound;
		public int health = 5;
		public float speed = 2f;
		public float jumpPower = 30f;
		public ParticleSystem jumpParticles;

		public bool isAlive { get; private set; }

		protected float horizontalVel = 0;

		private float lastShotTime = 0;

		public override void Start() {
			base.Start();
			isAlive = true;
			dieSound = SoundManager.Instance.GetEnemyDieSound();
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
			Vector3 nDir = ((Vector3) direction * shootOffset.x) + Vector3.Cross(Vector3.forward, direction).normalized * shootOffset.y;
			GameObject bul = Instantiate<GameObject>(bullet, transform.position + nDir, transform.localRotation, ActorManager.Instance.entitiesParent);
			bul.GetComponent<Bullet>().bounceCount = bounceCount;
			bul.GetComponent<Bullet>().damage = bulletDamage;
			bul.GetComponent<Rigidbody2D>().AddForce(direction * shotForce, ForceMode2D.Impulse);
			Physics2D.IgnoreCollision(bul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			//ParticleSystem syst = Instantiate<ParticleSystem>(shootParticles, transform.position + (Vector3) direction * shootMult, transform.localRotation, ActorManager.Instance.entitiesParent);
			shootParticles.transform.position = transform.position + nDir;
			shootParticles.transform.LookAt(transform.position + nDir * 2);
			shootParticles.Play();
			audioSrc.PlayOneShot(SoundManager.Instance.GetShootSound());
			GravityManager.Instance.RegisterEntity(bul);
		}

		public void Jump() {
			if (jumpParticles != null) {
				//jumpParticles.Play();
				ParticleSystem jumpEffect = Instantiate<ParticleSystem>(jumpParticles, transform.position + jumpParticles.transform.position, Quaternion.identity);
				jumpEffect.transform.LookAt(transform.position - localUp3 * 5 + jumpEffect.transform.position.z * Vector3.forward);
				jumpEffect.Play();
			}
			audioSrc.PlayOneShot(SoundManager.Instance.GetJumpSound());
			body.AddForce(localUp * jumpPower, ForceMode2D.Impulse);
		}

		public virtual void ApplyDamage(int damage) {
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
			if (OnDeath != null) {
				OnDeath.Invoke(this);
			}
			Destroy(gameObject);
			CallOnDestroy();
		}

	}
}
