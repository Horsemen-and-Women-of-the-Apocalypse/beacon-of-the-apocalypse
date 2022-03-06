using System.Collections;
using UnityEngine;

namespace Common {
    /// <summary>
    /// Component destroying the object after the specified period
    /// </summary>
    public class DestroyAfter : MonoBehaviour {
        [Tooltip("Lifetime of the object in seconds")]
        public float lifetime = 10;

        private void Start() {
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy() {
            // Wait lifetime
            yield return new WaitForSeconds(lifetime);

            // Destroy current object
            GameObject.Destroy(gameObject);
        }
    }
}