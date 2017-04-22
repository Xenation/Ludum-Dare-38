using UnityEngine;

namespace Assets.Scripts {
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

		protected static T _instance;
		public static T Instance {
			get {
				if (_instance == null) {
					_instance = FindObjectOfType<T>();
					if (_instance == null) {
						Debug.LogError("No instance of singleton " + typeof(T) + " found!");
					}
				}
				return _instance;
			}
		}

	}
}
