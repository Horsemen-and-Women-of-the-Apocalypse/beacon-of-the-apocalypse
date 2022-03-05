using UnityEngine;

/// <summary>
/// Component applying editor-specific modifications to the headset
/// </summary>
public class HeadsetStubbedController : MonoBehaviour {
    private const float RotationAnglePerFrame = 1;

    [Tooltip("Initial position of the camera when running the game in Unity Editor")]
    public Vector3 initialPosition = Vector3.zero;

    private Vector3 _rotation = Vector3.zero;

#if UNITY_EDITOR

    void Start() {
        transform.position = initialPosition;
    }

    void Update() {
        // Update rotation based on mouse movements
        _rotation.x -= RotationAnglePerFrame * Input.GetAxis("Mouse Y");
        _rotation.y += RotationAnglePerFrame * Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(_rotation);
    }

#endif
}