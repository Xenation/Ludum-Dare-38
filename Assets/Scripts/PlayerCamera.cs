using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
	public class PlayerCamera : MonoBehaviour {
		
		public Vector3 offset;
		public float lerpSpeed = 3f;

		// Use this for initialization
		public void Start() {
			
		}

		// Update is called once per frame
		public void Update() {
			if (!ActorManager.Instance.player.isAlive) return;
			Vector3 nPos = new Vector3();
			Vector3 relOffset = ActorManager.Instance.player.transform.TransformPoint(offset);
			nPos.x = Mathf.Lerp(transform.position.x, relOffset.x, Time.deltaTime * lerpSpeed);
			nPos.y = Mathf.Lerp(transform.position.y, relOffset.y, Time.deltaTime * lerpSpeed);
			nPos.z = Mathf.Lerp(transform.position.z, relOffset.z, Time.deltaTime * lerpSpeed);
			transform.position = nPos;
			GravityManager.Instance.Upright(transform);
		}
	}
}
