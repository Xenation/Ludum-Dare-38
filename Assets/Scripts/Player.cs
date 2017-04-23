using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class Player : Character {

		public bool isControlActive = true;

		private Dictionary<PowerupEffect, float> effects = new Dictionary<PowerupEffect, float>();

		// Use this for initialization
		public override void Start() {
			base.Start();
			dieSound = SoundManager.Instance.GetPlayerDieSound();
		}

		// Update is called once per frame
		public override void Update() {
			base.Update();
			UpdatePowerupEffects();
			if (!isControlActive) return;
			targetDir = GetMouseVec();
			if (Input.GetButton("right")) {
				horizontalVel = speed;
				sprite.flipX = false;
				arm.GetComponent<SpriteRenderer>().flipX = false;
			}
			if (Input.GetButton("left")) {
				horizontalVel = -speed;
				sprite.flipX = true;
				arm.GetComponent<SpriteRenderer>().flipX = true;
			}
			if (Input.GetButtonDown("jump") && !inAir) {
				Jump();
			}
			if (Input.GetButton("shoot")) {
				Shoot(((Vector2) targetDir).normalized);
			}
			//Debug.Log(inAir);
			//Debug.Log(GetHorizontalRelativeVelocity());
		}

		private Vector3 GetMouseVec() {
			return Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector3) body.position;
		}

		public void ApplyPowerupEffect(PowerupEffect effect) {
			effects.Add(effect, Time.time);
			effect.Apply(this);
		}

		private void UpdatePowerupEffects() {
			List<PowerupEffect> toRemove = new List<PowerupEffect>();
			foreach (KeyValuePair<PowerupEffect, float> effect in effects) {
				if (Time.time - effect.Value > effect.Key.GetDuration()) {
					effect.Key.Revert(this);
					toRemove.Add(effect.Key);
				}
			}
			foreach (PowerupEffect rem in toRemove) {
				effects.Remove(rem);
			}
		}
		
	}
}
