using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class Entity : MonoBehaviour {

		public delegate void DestroyEventHandler(Entity entity);
		public event DestroyEventHandler OnDestroy;

		public float groundDetectLength = .01f;
		public bool isUpright = true;
		public bool inAir { get; private set; }
		public Rigidbody2D body { get; private set; }
		public Vector2 localUp { get; private set; }
		public Vector3 localUp3 { get { return localUp; } }
		protected Vector3 right { get { return Vector3.Cross(localUp3, Vector3.forward).normalized; } }
		protected Vector2 right2 { get { return right; } }
		protected Vector2 movement = new Vector2();

		public bool UseGravity { get; protected set; }

		public Entity() {
			UseGravity = true;
		}

		public void Awake() {
			body = GetComponent<Rigidbody2D>();
		}

		// Use this for initialization
		public virtual void Start() {
			UpdateUp();
			inAir = true;
		}

		// Update is called once per frame
		public virtual void Update() {
			
		}
		
		public virtual void FixedUpdate() {
			if (GravityManager.Instance.center == null) return;
			UpdateUp();
			if (isUpright) {
				GravityManager.Instance.FixedUpright(body);
			}
			CheckOnGround();
		}

		private void UpdateUp() {
			localUp = (body.position - GravityManager.Instance.center.body.position).normalized;
		}

		public void CheckOnGround() {
			Debug.DrawLine(body.position, body.position - localUp * (1 + groundDetectLength), Color.red);
			RaycastHit2D[] hits = Physics2D.RaycastAll(body.position, -localUp, (1 + groundDetectLength));
			foreach (RaycastHit2D hit in hits) {
				if (hit.transform != transform) {
					inAir = false;
					return;
				}
			}
			inAir = true;
			//if (!hit) {
			//	inAir = true;
			//} else {
			//	inAir = false;
			//}
		}

		protected void SetHorizontalRelativeVelocity(float vel) {
			Vector2 negHoriz = Vector2.Dot(right, body.velocity) * right;
			body.velocity -= negHoriz;
			body.velocity += (Vector2) right * vel;
		}

		protected float GetHorizontalRelativeVelocity() {
			return Vector2.Dot(right, body.velocity);
		}

		protected void CallOnDestroy() {
			if (OnDestroy != null)
				OnDestroy.Invoke(this);
		}
	}
}
