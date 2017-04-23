using UnityEngine;
using UnityEditor;

using Assets.Scripts;

namespace Assets.Scripts.Editors {
	[CustomEditor(typeof(PlanetObject))]
	public class PlanetObjectEditor : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if (GUILayout.Button("Align")) {
				PlanetObject po = (PlanetObject) target;
				
				GravityManager.Instance.Upright(po.transform);
			}
		}

	}
}
