using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class Player : Actor {

		// Use this for initialization
		public override void Start() {
			base.Start();
		}

		// Update is called once per frame
		public override void Update() {
			base.Update();
			if (Input.GetButton("right")) {
				horizontalVel = speed;
			}
			if (Input.GetButton("left")) {
				horizontalVel = -speed;
			}
			if (Input.GetButtonDown("jump") && !inAir) {
				Jump();
			}
			if (Input.GetButtonDown("shoot")) {
				Shoot(((Vector2) GetMouseVec()).normalized);
			}
			//Debug.Log(inAir);
			//Debug.Log(GetHorizontalRelativeVelocity());
		}

		private Vector3 GetMouseVec() {
			return Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3) body.position;
		}
		
	}
}
