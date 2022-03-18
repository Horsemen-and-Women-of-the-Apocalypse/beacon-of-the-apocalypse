#nullable enable

using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Controller;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Implementation of <see cref="AControllerInput"/> based on the PICO controller
/// </summary>
public class PicoControllerInput : AControllerInput {
    private static readonly IReadOnlyList<InputFeatureUsage<Quaternion>> QuaternionFeatures = new[] { CommonUsages.deviceRotation };

    private static readonly IReadOnlyList<InputFeatureUsage<Vector2>> Vector2Features = new[] { CommonUsages.primary2DAxis };

    private static readonly IReadOnlyList<InputFeatureUsage<bool>> BooleanFeatures = new[]
        { CommonUsages.triggerButton, CommonUsages.menuButton, CommonUsages.primary2DAxisClick };

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

        // Notify trigger
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerState)) {
            Trigger.Invoke(triggerState);
        }

        // Notify menu button
        if (controller.TryGetFeatureValue(CommonUsages.menuButton, out var menuState)) {
            Menu.Invoke(menuState);
        }

        // Notify touch pad
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var touchPadState) && touchPadState &&
            controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out var touch)) {
            var x = Mathf.RoundToInt(touch.x);
            var y = Mathf.RoundToInt(touch.y);

            if (x == -1) {
                TouchPad.Invoke(ETouchPadButton.Left);
            } else if (x == 1) {
                TouchPad.Invoke(ETouchPadButton.Right);
            } else if (y == 1) {
                TouchPad.Invoke(ETouchPadButton.Top);
            } else if (y == -1) {
                TouchPad.Invoke(ETouchPadButton.Bottom);
            }
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
               BooleanFeatures.All(f => device.TryGetFeatureValue(f, out _)) &&
               Vector2Features.All(f => device.TryGetFeatureValue(f, out _));
    }
}