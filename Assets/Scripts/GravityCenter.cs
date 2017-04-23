using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	[ExecuteInEditMode]
	public class GravityCenter : MonoBehaviour {

		public Vector2 position {
			get {
				return transform.position;
			}
		}

		public Transform bodiesParent;
		public float gravityStrength = 10;
		public float mass = 10000;

		private List<Entity> affectedBodies;
		public Rigidbody2D body { get; private set; }
		public Collider2D coll { get; private set; }

		public GravityCenter() {
			affectedBodies = new List<Entity>();
		}

		public void Awake() {
			body = GetComponent<Rigidbody2D>();
			body.mass = mass;
		}
		
		// Use this for initialization
		public void Start() {
			InitAffectedBodies();
			coll = GetComponent<Collider2D>();
		}

		private void InitAffectedBodies() {
			for (int i = 0; i < bodiesParent.childCount; i++) {
				Entity entity = bodiesParent.GetChild(i).gameObject.GetComponent<Entity>();
				if (entity != null && entity.UseGravity) {
					affectedBodies.Add(entity);
					entity.OnDestroy += RemoveAffectedBody;
				}
			}
		}

		// Update is called once per frame
		public void FixedUpdate() {
			foreach (Entity entity in affectedBodies) {
				ApplyGravity(entity);
			}
		}

		private void ApplyGravity(Entity entity) {
			entity.body.velocity += GravityManager.Instance.GetGravityForce(entity) * -entity.localUp;
		}

		public void AddAffectedBody(Entity entity) {
			affectedBodies.Add(entity);
		}

		public void RemoveAffectedBody(Entity entity) {
			//Debug.Log("rem");
			affectedBodies.Remove(entity);
		}

	}
}
