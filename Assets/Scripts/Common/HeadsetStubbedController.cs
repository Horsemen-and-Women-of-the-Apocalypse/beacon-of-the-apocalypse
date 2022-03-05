using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Component applying editor-specific modifications to the headset
/// </summary>
public class HeadsetStubbedController : MonoBehaviour {
    [FormerlySerializedAs("InitialPosition"), Tooltip("Initial position of the camera when running the game in Unity Editor")]
    public Vector3 initialPosition = Vector3.zero;

#if UNITY_EDITOR

    void Start() {
        transform.position = initialPosition;
    }

#endif
}