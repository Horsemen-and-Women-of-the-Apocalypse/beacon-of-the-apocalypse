#nullable enable

using System.Collections.Generic;
using Common.Controller;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

/// <summary>
/// Event describing the rotation of the controller
/// </summary>
[System.Serializable]
public class RotationEvent : UnityEvent<Quaternion> { }

/// <summary>
/// Component exposing inputs of the Pico controller
/// </summary>
public class ControllerInput : MonoBehaviour {
    /// <summary>
    /// Event notifying controller rotation
    /// </summary>
    public RotationEvent Rotation;

    private InputDevice? _controller;

    void Start() {
        // Register connected devices
        var devices = new List<InputDevice>();
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
        if (!_controller.HasValue) {
            return;
        }

        var controller = _controller.Value;

        // Notify rotation
        if (controller.TryGetFeatureValue(CommonUsages.deviceRotation, out var rot)) {
            Rotation.Invoke(rot);
        }
    }

    private void OnDeviceConnected(InputDevice device) {
        if (PicoController.IsController(device)) {
            _controller = device;
        }
    }

    private void OnDeviceDisconnected(InputDevice device) {
        if (_controller == device) {
            _controller = null;
        }
    }
}