using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
	public class SequenceManager : Singleton<SequenceManager> {

		public enum FadeType {
			In,
			Out,
			Death
		}

		public GameObject faderIn;
		public GameObject faderOut;
		public GameObject faderDeath;
		public float fadeDuration;

		public Trigger endTrigger;

		public GameObject fader {
			get {
				switch (fadeType) {
					case FadeType.In:
						return faderIn;
					case FadeType.Out:
						return faderOut;
					case FadeType.Death:
						return faderDeath;
					default:
						return null;
				}
			}
		}

		public float DeltaPortion {
			get {
				return Time.deltaTime / fadeDuration;
			}
		}
		public float Portion {
			get {
				return (Time.time - fadeStart) / fadeDuration;
			}
		}

		private float fadeStart;
		private bool isFading = true;
		private FadeType fadeType = FadeType.In;

		public void Awake() {
			Fade(FadeType.In);
		}

		public void Start() {
			if (faderIn == null) return;
			fadeStart = Time.time;
			ActorManager.Instance.player.isControlActive = false;
			endTrigger.OnTrigger += OnEndTriggered;
			ActorManager.Instance.player.OnDeath += OnPlayerDeath;
		}

		public void Update() {
			if (faderIn == null) return;
			if (!isFading) return;
			Image img = fader.GetComponent<Image>();
			Color c = img.color;
			if (c.a + DeltaPortion * fadeType.GetFadeMult() > 1 || c.a + DeltaPortion * fadeType.GetFadeMult() < 0) {
				fader.SetActive(false);
				isFading = false;
				OnFadeEnd();
				return;
			}
			c.a += DeltaPortion * fadeType.GetFadeMult();
			img.color = c;
		}

		private void Fade(FadeType type) {
			if (faderIn == null) return;
			isFading = true;
			fadeType = type;
			if (!fader.activeInHierarchy) {
				fader.SetActive(true);
				Image img = fader.GetComponent<Image>();
				Color c = img.color;
				if (fadeType == FadeType.Out || fadeType == FadeType.Death) {
					c.a = 0;
				} else {
					c.a = 1;
				}
				img.color = c;
			}
			fadeStart = Time.time;
		}

		public void OnEndTriggered(Collider2D col) {
			if (ActorManager.Instance.player == null) return;
			if (col.transform == ActorManager.Instance.player.transform) {
				Fade(FadeType.Out);
			}
		}

		private void OnFadeEnd() {
			switch (fadeType) {
				case FadeType.In:
					// player control on
					ActorManager.Instance.player.isControlActive = true;
					break;
				case FadeType.Out:
					// load next scene
					if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
					}
					break;
				case FadeType.Death:
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
					break;
			}
		}

		public void OnPlayerDeath(Actor actor) {
			Fade(FadeType.Death);
		}

		public void Restart() {
			SceneManager.LoadScene(0);
		}

		public void Quit() {
			Application.Quit();
		}

	}
}
