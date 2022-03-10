using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace Multi {
    /// <summary>
    /// Component managing the player in multi mode
    /// </summary>
    public class Player : MonoBehaviour {
        /// <summary>
        /// Catch the given items
        /// </summary>
        /// <param name="items">Items to catch</param>
        public void CatchItems(IList<Item> items) {
#if UNITY_EDITOR
            Debug.Log($"[{string.Join(", ", items.Select(i => i.gameObject.name))}] caught");
#endif

            // TODO: Catch items (destroy them...)
        }
    }
}