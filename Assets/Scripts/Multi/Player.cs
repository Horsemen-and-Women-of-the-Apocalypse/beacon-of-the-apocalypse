using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Item;
using UnityEngine;

namespace Multi {
    /// <summary>
    /// Component managing the player in multi mode
    /// </summary>
    public class Player : MonoBehaviour {
        [Tooltip("Flashlight of the player")] public Flashlight flashlight;

        /// <summary>
        /// Catch the given items
        /// </summary>
        /// <param name="items">Items to catch</param>
        public void CatchItems(IList<AItem> items) {
            // Abort if there is no item
            if (items.Count == 0) {
                return;
            }

#if UNITY_EDITOR
            Debug.Log($"[{string.Join(", ", items.Select(i => i.gameObject.name))}] caught");
#endif

            // Use battery items
            var visitor = new UseBattery(flashlight);
            foreach (var item in items) {
                item.Visit(visitor);
            }
        }

        /// <summary>
        /// Visitor using only battery items
        /// </summary>
        private class UseBattery : AItem.IVisitor {
            private readonly Flashlight _flashlight;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="flashlight">Flashlight consuming the item</param>
            public UseBattery(Flashlight flashlight) {
                _flashlight = flashlight;
            }

            public void OnBattery(BatteryItem item) {
                _flashlight.Consume(item);
            }
        }
    }
}