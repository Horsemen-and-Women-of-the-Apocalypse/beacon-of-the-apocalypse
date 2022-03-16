using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common {
    /// <summary>
    /// Event describing the press of the main button of the headset
    /// </summary>
    [Serializable]
    public class ButtonPressedEvent : UnityEvent { }

    /// <summary>
    /// Event describing the long press of the main button of the headset
    /// </summary>
    [Serializable]
    public class ButtonLongPressedEvent : UnityEvent { }

    /// <summary>
    /// Component exposing headset inputs
    /// </summary>
    public class HeadsetInput : MonoBehaviour {
#if UNITY_EDITOR
        private const KeyCode ButtonKeyCode = KeyCode.A;
#else
        private const KeyCode ButtonKeyCode = KeyCode.JoystickButton0;
#endif

        [Tooltip("Duration in seconds required to consider an interaction with a button as a long press")]
        public float longPressDuration = 2;

        public ButtonPressedEvent onButtonPressed;

        public ButtonLongPressedEvent onButtonLongPressed;

        private float? _keyDownTimeStep;

        private void Update() {
            // Notify button press or long press
            if (Input.GetKeyDown(ButtonKeyCode)) {
                onButtonPressed.Invoke();
                _keyDownTimeStep ??= Time.time;

                // TODO: Display some feedback ?
            } else if (Input.GetKeyUp(ButtonKeyCode)) {
                if (_keyDownTimeStep == null) {
                    return;
                }

                if (Time.time - _keyDownTimeStep.Value < longPressDuration) {
                    _keyDownTimeStep = null;
                    return;
                }

                _keyDownTimeStep = null;
                onButtonLongPressed.Invoke();
            }
        }
    }
}