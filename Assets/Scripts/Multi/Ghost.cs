using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component managing the ghost player
/// </summary>
public class Ghost : MonoBehaviour {

	[Tooltip("Transform of the player wearing the headset")]
	public Transform PlayerTransform;
	[Tooltip("Distance (in meters) between the two players")]
	public float Distance = 4;
	
	private readonly Vector3 _initialControllerDirection = Vector3.forward;
	
	/// <summary>
	/// Update the position of the ghost based on the rotation of the controller
	/// </summary>
	/// <param name="rotation">Controller rotation</param>
    public void UpdatePosition(Quaternion rotation) {
        // Update ghost's position
        transform.position = -(rotation * _initialControllerDirection * Distance) + PlayerTransform.position;

        // TODO: Prevent ghost from going below the ground
    }
}
