using UnityEngine;

namespace Common {
    /// <summary>
    /// Component providing information about the item
    /// </summary>
    public class Item : MonoBehaviour, ITargetable {
        [Tooltip("Type of the item")] public Type type;

        public void OnEnter() {
            // TODO: Display borders
        }

        public void OnExit() {
            // TODO: Hide borders
        }

        /// <summary>
        /// Type of items
        /// </summary>
        public enum Type {
            Battery
        }
    }
}