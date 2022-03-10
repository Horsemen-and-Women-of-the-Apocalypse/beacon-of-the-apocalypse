using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common {
    /// <summary>
    /// Event listing the items caught by the player
    /// </summary>
    [Serializable]
    public class ItemsCatchingEvent : UnityEvent<IList<Item>> { }

    /// <summary>
    /// Component managing the collider of the flashlight and sending events about the objects that it detects
    /// </summary>
    public class Flashlight : MonoBehaviour {
        [Tooltip("Collider representing the flashlight")]
        public CapsuleCollider flashlightCollider;

        [Tooltip("Percentage taken by the collider in the spotlight")]
        public float spotlightPercentage = 33;

        [Tooltip("Light of the flashlight")] public Light flashlightLight;

        public ItemsCatchingEvent onItemCatching;

        private readonly Dictionary<int, GameObject> _inRangeObjectById = new Dictionary<int, GameObject>();

        private void Start() {
            // Adjust collider to fit the light
            var lightRange = flashlightLight.range;
            flashlightCollider.center = Vector3.forward * (lightRange / 2.0f + 0.5f);
            flashlightCollider.height = lightRange + 1;
            flashlightCollider.radius = Mathf.Tan(Mathf.Deg2Rad * flashlightLight.spotAngle / 2) * lightRange * (spotlightPercentage / 100.0f);
            flashlightCollider.direction = 2;
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
            var items = new List<Item>();

            // Iterate over a copy to be able to remove entries
            foreach (var idAndObject in new Dictionary<int, GameObject>(_inRangeObjectById)) {
                var @object = idAndObject.Value;

                // Add item if it's alive or remove it
                if (@object == null) {
                    _inRangeObjectById.Remove(idAndObject.Key);
                } else if (@object.TryGetComponent<Item>(out var item)) {
                    items.Add(item);
                }
            }

            // Notify items caught
            onItemCatching.Invoke(items);
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