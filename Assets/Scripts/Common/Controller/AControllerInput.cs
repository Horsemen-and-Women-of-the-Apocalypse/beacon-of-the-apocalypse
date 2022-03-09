using UnityEngine;

namespace Common.Controller {
    /// <summary>
    /// Base component for components exposing controller inputs
    /// </summary>
    public abstract class AControllerInput : MonoBehaviour {
        /// <summary>
        /// Event notifying controller rotation
        /// </summary>
        public RotationEvent Rotation;
    }
}