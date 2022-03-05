using UnityEngine;

/// <summary>
/// Component managing the ghost player
/// </summary>
public class Ghost : MonoBehaviour {
    private static readonly Vector3 InitialControllerDirection = Vector3.forward;

    [Tooltip("Transform of the player wearing the headset")]
    public Transform playerTransform;

    [Tooltip("Distance (in meters) between the two players")]
    public float distance = 4;

    [Tooltip("Maximum positive angle allowed on X axis")]
    public float maxPosAngle = 30;

    [Tooltip("Maximum negative angle allowed on X axis")]
    public float maxNegAngle = 15;

    private readonly float _medianAngle;
    private readonly float _minAngle;

    Ghost() {
        _minAngle = 360 - maxNegAngle;
        _medianAngle = maxPosAngle + Mathf.Abs(maxPosAngle - _minAngle) / 2;
    }

    /// <summary>
    /// Update the position of the ghost based on the rotation of the controller
    /// </summary>
    /// <param name="rotation">Controller rotation</param>
    public void UpdatePosition(Quaternion rotation) {
        // Limit rotation on X axis to prevent ghost from going below the ground...
        var angles = rotation.eulerAngles;
        var xAngles = angles.x;
        xAngles = xAngles >= _medianAngle ? Mathf.Clamp(xAngles, _minAngle, 359.999f) : Mathf.Clamp(xAngles, 0, maxPosAngle);

        // Update ghost's position
        transform.position = playerTransform.position - Quaternion.Euler(xAngles, angles.y, 0) * InitialControllerDirection * distance;
    }
}