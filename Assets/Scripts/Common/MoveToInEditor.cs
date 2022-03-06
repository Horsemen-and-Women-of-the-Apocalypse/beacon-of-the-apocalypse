using UnityEngine;

namespace Common {
    /// <summary>
    /// Component updating the initial position of the object when the game is running in Unity Editor
    /// </summary>
    public class MoveToInEditor : MonoBehaviour {
        [Tooltip("Initial position of the object when the game is running in Unity Editor")]
        public Vector3 initialPosition;

#if UNITY_EDITOR
        private void Start() {
            transform.position = initialPosition;
        }
#endif
    }
}