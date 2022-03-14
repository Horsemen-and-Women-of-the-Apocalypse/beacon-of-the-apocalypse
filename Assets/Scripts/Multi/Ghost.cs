#nullable enable

using System;
using System.Collections;
using Common;
using Common.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace Multi {
    /// <summary>
    /// Event describing the win of the game
    /// </summary>
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

        [Tooltip("Played when the ghost die")] public AudioSource dieSound;

        public float smoothTime = 0.1F;
        private Vector3 velocity = Vector3.zero;

        private Coroutine? _deathCoroutine;
        private Coroutine? _panicSoundFadeInCoroutine;
        private Coroutine? _panicSoundFadeOutCoroutine;
        private readonly float _medianAngle;
        private readonly float _minAngle;
        private bool _immune = true;

        Ghost() {
            _minAngle = 360 - maxNegAngle;
            _medianAngle = maxPosAngle + Mathf.Abs(maxPosAngle - _minAngle) / 2;
        }

        private void Start() {
            // Remove immunity
            StartCoroutine(RemoveImmunity());
        }

        private void Update() {
            // Ghosts always looks at the player
            var playerCamera = Camera.main;
            if (playerCamera != null) {
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

            // Update ghost's rotation and pos
            var targetPosition = playerTransform.position - Quaternion.Euler(xAngles, angles.y, 0) * InitialControllerDirection * distance;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
       
            var targetRotation = Quaternion.LookRotation(Vector3.Lerp(targetPosition, Camera.main.transform.position, 0.5f) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);


        }

        public void OnEnter() {
            // Do nothing if ghost is immune
            if (_immune) {
                return;
            }

            _deathCoroutine = StartCoroutine(Die());

            // Play panic animation/sound
            if (_panicSoundFadeOutCoroutine != null) {
                StopCoroutine(_panicSoundFadeOutCoroutine);
            }

            _panicSoundFadeInCoroutine = StartCoroutine(AudioFadeOut.FadeIn(pannicSound, 0.3f));
        }

        public void OnExit() {
            // Do nothing if ghost is immune
            if (_immune) {
                return;
            }

            // Cancel death
            if (_deathCoroutine != null) {
                StopCoroutine(_deathCoroutine);
            }

            // Cancel panic animation/sound
            if (_panicSoundFadeInCoroutine != null) {
                StopCoroutine(_panicSoundFadeInCoroutine);
            }

            _panicSoundFadeOutCoroutine = StartCoroutine(AudioFadeOut.FadeOut(pannicSound, 0.1f));
        }

        /// <summary>
        /// Coroutine killing the ghost after <see cref="lifetime"/> second(s)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Coroutine removing the immunity after 2 seconds
        /// </summary>
        /// <returns></returns>
        private IEnumerator RemoveImmunity() {
            yield return new WaitForSeconds(2);

            _immune = false;
        }
    }
}