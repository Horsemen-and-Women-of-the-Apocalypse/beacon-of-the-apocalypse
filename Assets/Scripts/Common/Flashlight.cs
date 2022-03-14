using System;
using System.Collections;
using System.Collections.Generic;
using Common.Item;
using UnityEngine;
using UnityEngine.Events;

namespace Common {
    /// <summary>
    /// Event listing the items caught by the player
    /// </summary>
    [Serializable]
    public class ItemsCatchingEvent : UnityEvent<IList<AItem>> { }

    /// <summary>
    /// Event describing the remaining percentage of battery
    /// </summary>
    [Serializable]
    public class RemainingBatteryPercentage : UnityEvent<float> { }

    /// <summary>
    /// Event representing a game over
    /// </summary>
    [Serializable]
    public class GameOverEvent : UnityEvent { }

    /// <summary>
    /// Component managing the collider of the flashlight and sending events about the objects that it detects
    /// </summary>
    public class Flashlight : MonoBehaviour {
        private const float MaximumBatteryLevel = 100;

        /// <summary>
        /// Initial level of battery
        /// </summary>
        public const float InitialBatteryLevel = MaximumBatteryLevel;

        /// <summary>
        /// Level of battery considered as critical
        /// </summary>
        public const float CriticalBatteryLevel = 25;

        [Tooltip("Frequency of battery level decrease in seconds")]
        public ushort batteryDecreaseTickFrequency = 10;

        [Tooltip("Percentage of battery to remove per tick")]
        public float batteryDecreasePerTick = 5;

        [Tooltip("Collider representing the flashlight")]
        public CapsuleCollider flashlightCollider;

        [Tooltip("Percentage taken by the collider in the spotlight")]
        public float spotlightPercentage = 33;

        [Tooltip("Light of the flashlight")] public Light flashlightLight;

        public ItemsCatchingEvent onItemCatching;

        public RemainingBatteryPercentage onBatteryChange;

        public GameOverEvent onGameOver;

        public AudioSource catchSound;

        [Tooltip("Played when the flashlight glitch a little")]
        public AudioSource defectingflashlightSound;

        [Tooltip("Played when the flashlight is turned off")]
        public AudioSource flashlightOffSound;

        private readonly Dictionary<int, GameObject> _inRangeObjectById = new Dictionary<int, GameObject>();
        private float _batteryLevel = InitialBatteryLevel;

        private void Start() {
            // Adjust collider to fit the light
            var lightRange = flashlightLight.range;
            flashlightCollider.center = Vector3.forward * (lightRange / 2.0f + 0.5f);
            flashlightCollider.height = lightRange + 1;
            flashlightCollider.radius = Mathf.Tan(Mathf.Deg2Rad * flashlightLight.spotAngle / 2) * lightRange * (spotlightPercentage / 100.0f);
            flashlightCollider.direction = 2;

            // Start battery level decrease
            StartCoroutine(Consume());
        }

        private void OnTriggerEnter(Collider other) {
            var triggerObject = other.gameObject;

#if UNITY_EDITOR
            Debug.Log($"{triggerObject.name} is in range");
#endif
            // Notify enter
            if (triggerObject.TryGetComponent<ITargetable>(out var target)) {
                target.OnEnter();
            }

            _inRangeObjectById[triggerObject.GetInstanceID()] = triggerObject;
        }

        private void OnTriggerExit(Collider other) {
            var triggerObject = other.gameObject;

#if UNITY_EDITOR
            Debug.Log($"{triggerObject.name} is out-of-range");
#endif
            // Notify exit
            if (triggerObject.TryGetComponent<ITargetable>(out var target)) {
                target.OnExit();
            }

            _inRangeObjectById.Remove(triggerObject.GetInstanceID());
        }

        /// <summary>
        /// Catch items in spotlight
        /// </summary>
        public void CatchItems() {
            var items = new List<AItem>();

            // Iterate over a copy to be able to remove entries
            foreach (var idAndObject in new Dictionary<int, GameObject>(_inRangeObjectById)) {
                var @object = idAndObject.Value;

                // Remove destroyed objects & add item
                if (@object == null) {
                    _inRangeObjectById.Remove(idAndObject.Key);
                } else if (@object.TryGetComponent<AItem>(out var item)) {
                    items.Add(item);
                    // Play sound
                    if (catchSound != null) catchSound.Play();
                }
            }

            // Notify items caught
            onItemCatching.Invoke(items);
        }

        /// <summary>
        /// Decrease the level of the battery by <see cref="batteryDecreasePerTick"/>
        /// </summary>
        /// <returns>New battery level</returns>
        private float Decrease() {
            var level = _batteryLevel = Mathf.Max(0, _batteryLevel - batteryDecreasePerTick);

            if (level > CriticalBatteryLevel) {
                return level;
            }

            // Turn off the light & send game over
            if (level <= 0) {
                flashlightLight.intensity = 0;
                flashlightOffSound.Play();
                onGameOver.Invoke();
                return level;
            }

            // Make the flashlight blink
            StartCoroutine(Blink());
            return level;
        }

        /// <summary>
        /// Increase the level of the battery by the given amount
        /// </summary>
        /// <param name="level">Amount to add</param>
        /// <returns>New battery level</returns>
        private float Increase(float level) {
            return _batteryLevel = Mathf.Min(MaximumBatteryLevel, _batteryLevel + level);
        }

        /// <summary>
        /// Coroutine consuming the battery over time
        /// </summary>
        /// <returns></returns>
        private IEnumerator Consume() {
            while (isActiveAndEnabled && _batteryLevel > 0) {
                // Wait tick
                yield return new WaitForSeconds(batteryDecreaseTickFrequency);

                // Decrease and notify battery level
                onBatteryChange.Invoke(Decrease());
#if UNITY_EDITOR
                Debug.Log($"Battery: {_batteryLevel}%");
#endif
            }
        }

        /// <summary>
        /// Consume the given battery item
        /// </summary>
        /// <param name="item">Item</param>
        public void Consume(BatteryItem item) {
            var itemGameObject = item.gameObject;

            // Do nothing if item is destroyed
            if (itemGameObject == null) {
                return;
            }

            // Use item
            onBatteryChange.Invoke(Increase(item.level));
#if UNITY_EDITOR
            Debug.Log($"Battery: {_batteryLevel}%");
#endif
            Destroy(itemGameObject);
        }

        /// <summary>
        /// Coroutine making the flashlight blink
        /// </summary>
        /// <returns></returns>
        private IEnumerator Blink() {
            var intensity = flashlightLight.intensity;

            // Make the light blink a random amount of times
            var nbBlink = UnityEngine.Random.Range(2, 7);
            for (var i = 0; i < nbBlink; i++) {
                // Turn off
                flashlightLight.intensity = 0;
                defectingflashlightSound.Play();

                // Wait a bit
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.15f));

                // Restore intensity
                flashlightLight.intensity = intensity;
                defectingflashlightSound.Play();

                // Wait a bit
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.02f, 0.05f));
            }
        }
    }

    /// <summary>
    /// Interface for objects that can be a target of the flashlight
    /// </summary>
    public interface ITargetable {
        /// <summary>
        /// Method called when this object enters the spotlight
        /// </summary>
        public void OnEnter();

        /// <summary>
        /// Method called when this object exists the spotlight
        /// </summary>
        public void OnExit();
    }
}