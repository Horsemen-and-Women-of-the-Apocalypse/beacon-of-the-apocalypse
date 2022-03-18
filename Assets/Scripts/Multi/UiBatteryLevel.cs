using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Multi {
    /// <summary>
    /// Component managing the battery level displayed in a canvas
    /// </summary>
    public class UiBatteryLevel : MonoBehaviour {
        [Tooltip("Text displaying the battery level")]
        public Text text;

        private void Start() {
            text.text = ToString(Flashlight.InitialBatteryLevel);
        }

        /// <summary>
        /// Update the level
        /// </summary>
        /// <param name="level">New battery level</param>
        public void UpdateLevel(float level) {
            // Update text
            text.text = ToString(level);

            // Update color
            text.color = level <= Flashlight.CriticalBatteryLevel ? Color.red : Color.white;
        }

        /// <summary>
        /// Generate the string representation of the battery level
        /// </summary>
        /// <param name="level">Level</param>
        /// <returns>String representation</returns>
        private static string ToString(float level) {
            return $"{Mathf.RoundToInt(level)} %";
        }
    }
}