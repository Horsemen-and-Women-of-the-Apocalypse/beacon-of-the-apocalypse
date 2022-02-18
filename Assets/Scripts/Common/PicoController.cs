using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Event describing the rotation of the controller
/// </summary>
[System.Serializable]
public class RotationEvent : UnityEvent<Quaternion> {  }

/// <summary>
/// Component exposing inputs of the Pico controller
/// </summary>
public class PicoController : MonoBehaviour {
    
	private static readonly IReadOnlyList<InputFeatureUsage<Quaternion>> QuaternionFeatures  = new List<InputFeatureUsage<Quaternion>>(new [] { CommonUsages.deviceRotation });
	private static readonly IReadOnlyList<InputFeatureUsage<bool>> BooleanFeatures = new List<InputFeatureUsage<bool>>(new []{ CommonUsages.triggerButton });

    /// <summary>
    /// Event notifying controller rotation
    /// </summary>
	public RotationEvent Rotation;

	private InputDevice? _device = null;

    void Start() {
        // Register connected devices
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        foreach (var device in devices) {
            OnDeviceConnected(device);
        }

        // Subscribe to device events
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
    }

    void OnDestroy() {
        // Unsubscribe to device events
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceDisconnected -= OnDeviceDisconnected;
    }

    void Update() {
		// Check if the controller is detected
        if (_device is null) {
            return;
        }
        InputDevice device = _device.Value;
        
        // Notify rotation
        if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out var rot)) {
            Rotation.Invoke(rot);
        }
    }

    private void OnDeviceConnected(InputDevice device) {
        // Check if device meets the requirements
        if (QuaternionFeatures.All(f => device.TryGetFeatureValue(f, out var _)) && BooleanFeatures.All(f => device.TryGetFeatureValue(f, out var _))) {
            _device = device;
        }
    }

    private void OnDeviceDisconnected(InputDevice device) {
        if (_device == device) {
            _device = null;
        }
    }
}
