using UnityEngine;

namespace Assets.Scripts {
	public static class Extensions {

		public static int GetFadeMult(this SequenceManager.FadeType type) {
			switch (type) {
				case SequenceManager.FadeType.In:
					return -1;
				case SequenceManager.FadeType.Out:
				case SequenceManager.FadeType.Death:
					return 1;
				default:
					return 1;
			}
		}

	}
}
