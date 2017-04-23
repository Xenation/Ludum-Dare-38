using UnityEngine;

namespace Assets.Scripts {
	public class Trigger : MonoBehaviour {

		public delegate void OnTriggerHandler(Collider2D col);
		public event OnTriggerHandler OnTrigger;

		public void OnTriggerEnter2D(Collider2D col) {
			if (OnTrigger != null) {
				OnTrigger.Invoke(col);
			}
		}

	}
}
