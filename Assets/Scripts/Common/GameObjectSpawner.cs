using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Common {
    /// <summary>
    /// Component spawning objects
    /// </summary>
    public class GameObjectSpawner : MonoBehaviour {
        public readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        [Tooltip("Parent of spawned objects")] public GameObject parent;

        [Tooltip("Game object to spawn")] public GameObject target;

        [Tooltip("Frequency of spawn wave in seconds")]
        public float waveFrequency = 10;

        [Tooltip("Number of objects to spawn per wave")]
        public int numberOfSpawnPerWave = 2;

        [Tooltip("If a spawn should create an object one at a time")]
        public bool oneAtATime = true;

        [Tooltip("Radius of spawns")] public float spawnRadius = 2;

        [Tooltip("Lists of spawn locations")] public List<Vector2> spawns = new List<Vector2>();

        private GameObject[] _objects;

        void Start() {
            _objects = new GameObject[spawns.Count];
            StartCoroutine(SpawnObjects());
        }

        /// <summary>
        /// Coroutine spawning objects
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnObjects() {
            var bytes = new byte[2];

            while (isActiveAndEnabled) {
                // Wait frequency
                yield return new WaitForSeconds(waveFrequency);

                for (var i = 0; i < numberOfSpawnPerWave; i++) {
                    // Generate name
                    var objectName = Guid.NewGuid().ToString();
                    var hashCode = objectName.GetHashCode();
                    yield return null;

                    // Pick spawn
                    int spawnIndex;
                    if (oneAtATime) {
                        GameObject spawned;
                        do {
                            // Randomize hashCode at each try
                            RandomNumberGenerator.GetBytes(bytes);
                            hashCode += BitConverter.ToInt16(bytes, 0);

                            // Select spawn
                            var index = Math.Abs(hashCode) % spawns.Count;
                            spawned = _objects[index];
                            spawnIndex = index;

                            yield return null;
                        } while (spawned != null);
                    } else {
                        spawnIndex = Math.Abs(hashCode) % spawns.Count;
                        yield return null;
                    }

                    // Get and randomize position
                    var angle = Mathf.Deg2Rad * hashCode;
                    var position = spawns[spawnIndex] + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) *
                        (Mathf.InverseLerp(int.MinValue, int.MaxValue, hashCode) * spawnRadius);
                    yield return null;

                    // Instantiate object
                    var @object = _objects[spawnIndex] = Instantiate(target, parent.transform, false);
                    @object.transform.localPosition = new Vector3(position.x, 0, position.y);
                    @object.name = objectName;
                    yield return null;
                }
            }
        }
    }
}