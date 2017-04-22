using UnityEngine;
using UnityEditor;

namespace Assets.Scripts {
	[CustomEditor(typeof(PlanetObject))]
	public class PlanetObjectEditor : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if (GUILayout.Button("Align")) {
				Debug.Log("target " + target.GetType());
				PlanetObject po = (PlanetObject) target;
				
				GravityManager.Instance.Upright(po.transform);
			}
		}

	}
}
