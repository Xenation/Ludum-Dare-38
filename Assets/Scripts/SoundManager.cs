using UnityEngine;

namespace Assets.Scripts {
	public class SoundManager : Singleton<SoundManager> {

		public AudioClip[] shootSounds;
		public AudioClip[] enemyDieSounds;
		public AudioClip[] playerDieSounds;
		public AudioClip[] hitSounds;
		public AudioClip[] jumpSounds;
		public AudioClip[] powerupSounds;

		public AudioClip GetShootSound() {
			return RandomSoundIn(shootSounds);
		}

		public AudioClip GetEnemyDieSound() {
			return RandomSoundIn(enemyDieSounds);
		}

		public AudioClip GetPlayerDieSound() {
			return RandomSoundIn(playerDieSounds);
		}

		public AudioClip GetHitSound() {
			return RandomSoundIn(hitSounds);
		}

		public AudioClip GetJumpSound() {
			return RandomSoundIn(jumpSounds);
		}

		public AudioClip GetPowerupSound() {
			return RandomSoundIn(powerupSounds);
		}

		private AudioClip RandomSoundIn(AudioClip[] sounds) {
			if (sounds.Length == 0) return null;
			return sounds[Random.Range(0, sounds.Length)];
		}

	}
}
