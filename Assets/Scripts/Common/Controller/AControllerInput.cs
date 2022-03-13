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

        /// <summary>
        /// Event notifying the state of the trigger button
        /// </summary>
        public TriggerEvent Trigger;
        
        /// <summary>
        /// Event notifying a click on the touch pad
        /// </summary>
        public TouchPadClickEvent TouchPad;

        /// <summary>
        /// Event notifying the state of the menu button
        /// </summary>
        public MenuButtonEvent Menu;
    }
}