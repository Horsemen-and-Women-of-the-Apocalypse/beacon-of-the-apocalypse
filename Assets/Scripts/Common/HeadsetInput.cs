using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common {
    /// <summary>
    /// Event describing the press of the main button on the headset
    /// </summary>
    [Serializable]
    public class ButtonPressedEvent : UnityEvent { }

    /// <summary>
    /// Component exposing headset inputs
    /// </summary>
    public class HeadsetInput : MonoBehaviour {
#if UNITY_EDITOR
        private const KeyCode ButtonKeyCode = KeyCode.A;
#else
        private const KeyCode ButtonKeyCode = KeyCode.JoystickButton0;
#endif

        public ButtonPressedEvent onButtonPressed;

        private void Update() {
            // Notify button's state
            if (Input.GetKey(ButtonKeyCode)) {
                onButtonPressed.Invoke();
            }
        }
    }
}