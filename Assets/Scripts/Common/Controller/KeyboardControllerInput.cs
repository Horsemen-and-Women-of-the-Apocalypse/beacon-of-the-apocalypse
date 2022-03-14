using UnityEngine;

namespace Common.Controller {
    /// <summary>
    /// Implementation of <see cref="AControllerInput"/> based on keyboard
    /// </summary>
    public class KeyboardControllerInput : AControllerInput {
        private const float RotationAnglePerFrame = 1;

        private Vector3 _rotation = Vector3.zero;

        private void Start() {
            transform.localPosition = new Vector3(0.25f, -0.5f);
        }

        private void Update() {
            // Update rotation
            if (Input.GetKey(KeyCode.UpArrow)) {
                _rotation.x -= RotationAnglePerFrame;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                _rotation.x += RotationAnglePerFrame;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                _rotation.y += RotationAnglePerFrame;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                _rotation.y -= RotationAnglePerFrame;
            }

            // Notify rotation
            var rotationAsQuaternion = Quaternion.Euler(_rotation);
            Rotation.Invoke(rotationAsQuaternion);

            // Notify trigger
            Trigger.Invoke(Input.GetKey(KeyCode.Z));

            // Notify menu button
            Menu.Invoke(Input.GetKey(KeyCode.E));

            // Touch pad
            {
                if (Input.GetKeyDown(KeyCode.O)) {
                    TouchPad.Invoke(ETouchPadButton.Top);
                } else if (Input.GetKeyDown(KeyCode.K)) {
                    TouchPad.Invoke(ETouchPadButton.Left);
                } else if (Input.GetKeyDown(KeyCode.L)) {
                    TouchPad.Invoke(ETouchPadButton.Bottom);
                } else if (Input.GetKeyDown(KeyCode.M)) {
                    TouchPad.Invoke(ETouchPadButton.Right);
                }
            }

            // Update object's rotation
            transform.localRotation = rotationAsQuaternion;
        }
    }
}