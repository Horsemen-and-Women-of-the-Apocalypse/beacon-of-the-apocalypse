using UnityEngine;

/// <summary>
/// Component applying editor-specific modifications to the headset
/// </summary>
public class HeadsetStubbedController : MonoBehaviour {
    private const float RotationAnglePerFrame = 1;

    private Vector3 _rotation = Vector3.zero;

#if UNITY_EDITOR

    void Update() {
        // Update rotation based on mouse movements
        _rotation.x -= RotationAnglePerFrame * Input.GetAxis("Mouse Y");
        _rotation.y += RotationAnglePerFrame * Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(_rotation);
    }

#endif
}