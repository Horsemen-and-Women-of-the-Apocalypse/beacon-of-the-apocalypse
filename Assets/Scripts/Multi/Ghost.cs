#nullable enable

using System;
using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.Events;

namespace Multi {
    // TODO: Doc
    [Serializable]
    public class WinEvent : UnityEvent { }

    /// <summary>
    /// Component managing the ghost player
    /// </summary>
    public class Ghost : MonoBehaviour, ITargetable {
        private static readonly Vector3 InitialControllerDirection = Vector3.forward;

        [Tooltip("Transform of the player wearing the headset")]
        public Transform playerTransform;

        [Tooltip("Lifetime in the flashlight")]
        public float lifetime = 10;

        [Tooltip("Distance (in meters) between the two players")]
        public float distance = 4;

        [Tooltip("Maximum positive angle allowed on X axis")]
        public float maxPosAngle = 30;

        [Tooltip("Maximum negative angle allowed on X axis")]
        public float maxNegAngle = 15;

        public WinEvent onWin;

        [Tooltip("Played when the ghost is being captured")]
        public AudioSource pannicSound;
     [Tooltip("Played when the ghost diee")]
        public AudioSource dieSound;
        private Coroutine? _deathCoroutine;
        private Coroutine? _pannicSoundFadeInCoroutine;
        private Coroutine? _pannicSoundFadeOuiCoroutine;
        private readonly float _medianAngle;
        private readonly float _minAngle;

        Ghost() {
            _minAngle = 360 - maxNegAngle;
            _medianAngle = maxPosAngle + Mathf.Abs(maxPosAngle - _minAngle) / 2;
        }

        private void Update() {
            // Ghosts always looks at the player
            var playerCamera = Camera.main;
            if (playerCamera != null) {
                transform.LookAt(playerCamera.transform);
            }
        }

        /// <summary>
        /// Update the position of the ghost based on the rotation of the controller
        /// </summary>
        /// <param name="rotation">Controller rotation</param>
        public void UpdatePosition(Quaternion rotation) {
            // Limit rotation on X axis to prevent ghost from going below the ground...
            var angles = rotation.eulerAngles;
            var xAngles = angles.x;
            xAngles = xAngles >= _medianAngle ? Mathf.Clamp(xAngles, _minAngle, 359.999f) : Mathf.Clamp(xAngles, 0, maxPosAngle);

            // Update ghost's position
            transform.position = playerTransform.position - Quaternion.Euler(xAngles, angles.y, 0) * InitialControllerDirection * distance;
        }

        public void OnEnter() {
            _deathCoroutine = StartCoroutine(Die());
            
            // Play panic animation/sound
            if (_pannicSoundFadeOuiCoroutine != null) {
                StopCoroutine(_pannicSoundFadeOuiCoroutine);
            }
            _pannicSoundFadeInCoroutine = StartCoroutine (Common.Audio.AudioFadeOut.FadeIn (pannicSound, 0.3f));
        }

        public void OnExit() {
            // Cancel death
            if (_deathCoroutine != null) {
                StopCoroutine(_deathCoroutine);
            }
            
            // Cancel panic animation/sound
            if (_pannicSoundFadeInCoroutine != null) {
                StopCoroutine(_pannicSoundFadeInCoroutine);
            }
            StartCoroutine (Common.Audio.AudioFadeOut.FadeOut (pannicSound, 0.1f));
        }

        // TODO: Doc
        private IEnumerator Die() {
            // Wait
            yield return new WaitForSeconds(lifetime);

            // Play an animation/sound ???
            pannicSound.Stop();
            AudioSource.PlayClipAtPoint(dieSound.clip, transform.position);
    
            // Destroy ghost
            Destroy(gameObject);

            // Send win
            onWin.Invoke();
        }
    }
}