namespace Common.Controller {
    /// <summary>
    /// Component exposing controller inputs
    /// </summary>
    public class ControllerInput :
#if UNITY_EDITOR
        KeyboardControllerInput { }
#else
        PicoControllerInput { }
#endif
}