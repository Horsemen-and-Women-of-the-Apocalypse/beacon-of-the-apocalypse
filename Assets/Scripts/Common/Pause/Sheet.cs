using System.Collections;
using UnityEngine;

namespace Common.Pause
{
    public class Sheet : MonoBehaviour
    {
        private float distance = 1.25f;

        private int speed = 5;

        private Coroutine _lookCoroutine;
        private Camera camera;

        public void StartFollowing()
        {
            if (!camera) camera = Camera.main;
            if (_lookCoroutine != null)
            {
                StopCoroutine(_lookCoroutine);
            }
        
            _lookCoroutine = StartCoroutine(Follow());
        }

        /// <summary>
        /// Follow camera's facing coroutine
        /// </summary>
        /// <returns></returns>
        private IEnumerator Follow()
        {
            while (true)
            {
                var cameraTransform = camera.transform;
                transform.position = cameraTransform.position 
                                     + Vector3.Scale(new Vector3(1f, 0f, 1f), cameraTransform.forward) * distance;
                
                Vector3 relativePos = transform.position - cameraTransform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                
                Quaternion current = transform.localRotation;
                
                transform.localRotation = Quaternion.Slerp(current, rotation, Time.unscaledDeltaTime                              
                                                                              * speed);
                yield return null;
            }
        }
    }
} 