using System;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Controller {
    /// <summary>
    /// Event describing the rotation of the controller
    /// </summary>
    [Serializable]
    public class RotationEvent : UnityEvent<Quaternion> { }

    /// <summary>
    /// Event describing the state of the trigger
    /// </summary>
    [Serializable]
    public class TriggerEvent : UnityEvent<bool> { }

    /// <summary>
    /// Event describing a click on the touch pad
    /// </summary>
    [Serializable]
    public class TouchPadClickEvent : UnityEvent<ETouchPadButton> { }

    /// <summary>
    /// Event describing the state of the menu button
    /// </summary>
    [Serializable]
    public class MenuButtonEvent : UnityEvent<bool> { }

    /// <summary>
    /// Possible buttons on the touch pad
    /// </summary>
    public enum ETouchPadButton {
        Top,
        Bottom,
        Left,
        Right
    }
}