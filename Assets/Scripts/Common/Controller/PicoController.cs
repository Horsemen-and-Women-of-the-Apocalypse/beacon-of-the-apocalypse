using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace Common.Controller {
    /// <summary>
    /// Class exposing methods related to the PICO controller
    /// </summary>
    public static class PicoController {
        private static readonly IReadOnlyList<InputFeatureUsage<Quaternion>> QuaternionFeatures =
            new List<InputFeatureUsage<Quaternion>>(new[] { CommonUsages.deviceRotation });

        private static readonly IReadOnlyList<InputFeatureUsage<bool>> BooleanFeatures =
            new List<InputFeatureUsage<bool>>(new[] { CommonUsages.triggerButton });

        /// <summary>
        /// Test whether the given device is the controller of the PICO
        /// </summary>
        /// <param name="device">Device to test</param>
        /// <returns>true if the device is the controller of PICO, otherwise false</returns>
        public static bool IsController(InputDevice device) {
            return QuaternionFeatures.All(f => device.TryGetFeatureValue(f, out _)) &&
                   BooleanFeatures.All(f => device.TryGetFeatureValue(f, out _));
        }
    }
}