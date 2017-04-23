using UnityEngine;

namespace Assets.Scripts {
	[ExecuteInEditMode]
	public class PlanetObject : MonoBehaviour {

		public void Update() {
			GravityManager.Instance.Upright(transform);
		}

	}
}
