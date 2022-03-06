using UnityEngine;

namespace Common {
    /// <summary>
    /// Component rotating (only on Y axis) the object at the start so that it looks at the camera
    /// </summary>
    public class LookAtCamera : MonoBehaviour {
        void Start() {
            // Get camera
            var playerCamera = Camera.main;
            if (playerCamera == null) {
                return;
            }

            // Rotate object
            var objectTransform = transform;
            objectTransform.LookAt(playerCamera.transform);

            // Keep only rotation on Y axis
            objectTransform.rotation = Quaternion.Euler(0, objectTransform.rotation.eulerAngles.y, 0);
        }
    }
}