using UnityEngine;

namespace Assets.Scripts {
	public class OneShotParticleSystem : MonoBehaviour {

		private ParticleSystem syst;

		public void Start() {
			syst = GetComponent<ParticleSystem>();
		}

		public void Update() {
			if (!syst.IsAlive()) {
				Destroy(gameObject);
			}
		}

	}
}
