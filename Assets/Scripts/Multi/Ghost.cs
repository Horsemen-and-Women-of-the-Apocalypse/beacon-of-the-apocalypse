using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Component managing the ghost player
/// </summary>
public class Ghost : MonoBehaviour {
    [FormerlySerializedAs("PlayerTransform"), Tooltip("Transform of the player wearing the headset")]
    public Transform playerTransform;

    [FormerlySerializedAs("Distance"), Tooltip("Distance (in meters) between the two players")]
    public float distance = 4;

    private readonly Vector3 _initialControllerDirection = Vector3.forward;

    /// <summary>
    /// Update the position of the ghost based on the rotation of the controller
    /// </summary>
    /// <param name="rotation">Controller rotation</param>
    public void UpdatePosition(Quaternion rotation) {
        // Update ghost's position
        transform.position = -(rotation * _initialControllerDirection * distance) + playerTransform.position;

        // TODO: Prevent ghost from going below the ground
    }
}