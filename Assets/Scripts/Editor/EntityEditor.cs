using UnityEngine;
using UnityEditor;

using Assets.Scripts;

namespace Assets.Scripts.Editors {
	[CustomEditor(typeof(Entity))]
	public class EntityEditor : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if (GUILayout.Button("Align")) {
				Entity ent = (Entity) target;

				GravityManager.Instance.Upright(ent.transform);
			}
		}

	}

	[CustomEditor(typeof(Actor))]
	public class ActorEditor : EntityEditor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
		}
	}

	[CustomEditor(typeof(Player))]
	public class PlayerEditor : EntityEditor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
		}
	}

	[CustomEditor(typeof(Powerup))]
	public class PowerupEditor : EntityEditor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
		}
	}

	[CustomEditor(typeof(Enemy))]
	public class EnemyEditor : EntityEditor {
		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
		}
	}
}
