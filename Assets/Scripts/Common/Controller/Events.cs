using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Controller {
    /// <summary>
    /// Event describing the rotation of the controller
    /// </summary>
    [Serializable]
    public class RotationEvent : UnityEvent<Quaternion> { }
}