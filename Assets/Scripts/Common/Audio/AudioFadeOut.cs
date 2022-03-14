using UnityEngine;
using System.Collections;

namespace Common.Audio {
    public static class AudioFadeOut {
        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            // audioSource.Stop ();
            audioSource.volume = 0f;
        }


        public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime) {
            // audioSource.Play ();
            audioSource.volume = 0f;

            while (audioSource.volume < 1) {
                audioSource.volume += Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.volume = 1f;
        }
    }
}