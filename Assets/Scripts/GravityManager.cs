using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class GravityManager : Singleton<GravityManager> {

		public static readonly float G = 6.67408E-11f;

		public float gravityMultiplier = 10000;
		public GravityCenter center;

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}

		public Vector2 GetLocalUp(Vector2 pos) {
			return (pos - center.body.position).normalized;
		}

		public void Upright(Transform transf) {
			transf.LookAt(transf.position + Vector3.forward, GetLocalUp(transf.position));
		}

		public void FixedUpright(Rigidbody2D rigid) {
			//rigid.transform.LookAt(((Vector3) rigid.position) + Vector3.forward, GetLocalUp(rigid.position));
			rigid.rotation = Quaternion.LookRotation(Vector3.forward, GetLocalUp(rigid.position)).eulerAngles.z;
		}

		public float GetGravityForce(Entity actor) {
			return G * ((actor.body.mass * center.body.mass) / Vector3.Distance(center.body.position, actor.body.position)) * gravityMultiplier;
		}

		public void RegisterEntity(GameObject go) {
			Entity ent = go.GetComponent<Entity>();
			if (ent == null) return;
			if (!ent.UseGravity) return;
			center.AddAffectedBody(ent);
			ent.OnDestroy += center.RemoveAffectedBody;
		}

	}
}
