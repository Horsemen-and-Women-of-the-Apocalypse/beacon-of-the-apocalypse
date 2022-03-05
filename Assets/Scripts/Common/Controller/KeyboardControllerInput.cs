using UnityEngine;

namespace Common.Controller {
    /// <summary>
    /// Implementation of <see cref="AControllerInput"/> based on keyboard
    /// </summary>
    public class KeyboardControllerInput : AControllerInput {
        private const float RotationAnglePerFrame = 1;

        private Vector3 _rotation = Vector3.zero;

        private void Update() {
            // Update rotation
            if (Input.GetKey(KeyCode.UpArrow)) {
                _rotation.x += RotationAnglePerFrame;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                _rotation.x -= RotationAnglePerFrame;
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                _rotation.y += RotationAnglePerFrame;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                _rotation.y -= RotationAnglePerFrame;
            }

            // Notify rotation
            Rotation.Invoke(Quaternion.Euler(_rotation));
        }
    }
}