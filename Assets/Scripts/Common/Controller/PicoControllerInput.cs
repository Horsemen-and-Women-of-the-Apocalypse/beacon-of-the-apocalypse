#nullable enable

using System.Collections.Generic;
using System.Linq;
using Common.Controller;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Implementation of <see cref="AControllerInput"/> based on the PICO controller
/// </summary>
public class PicoControllerInput : AControllerInput {
    private static readonly IReadOnlyList<InputFeatureUsage<Quaternion>> QuaternionFeatures =
        new List<InputFeatureUsage<Quaternion>>(new[] { CommonUsages.deviceRotation });

    private static readonly IReadOnlyList<InputFeatureUsage<bool>> BooleanFeatures =
        new List<InputFeatureUsage<bool>>(new[] { CommonUsages.triggerButton });

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
        if (IsController(device)) {
            _controller = device;
        }
    }

    private void OnDeviceDisconnected(InputDevice device) {
        if (_controller == device) {
            _controller = null;
        }
    }

    /// <summary>
    /// Test whether the given device is the controller of the PICO
    /// </summary>
    /// <param name="device">Device to test</param>
    /// <returns>true if the device is the controller of PICO, otherwise false</returns>
    private static bool IsController(InputDevice device) {
        return QuaternionFeatures.All(f => device.TryGetFeatureValue(f, out _)) &&
               BooleanFeatures.All(f => device.TryGetFeatureValue(f, out _));
    }
}