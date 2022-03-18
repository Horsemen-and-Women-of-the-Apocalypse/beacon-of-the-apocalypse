using System.Collections;
using Common.Pause;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Multi
{
    public class MultiPauseMenu : PauseMenu
    {
        public GameObject instructionText;
        public Text title;
        public GameObject buttons;

        public int count = 5;
        public float interval = 1;
        public AudioSource tickSound;

        public void ResetSheet()
        {
            title.text = "Exercise paused";
            buttons.SetActive(true);
            instructionText.SetActive(false);
        }
        
        public override void Resume()
        {
            PlayButtonSound();
            
            buttons.SetActive(false);
            instructionText.SetActive(true);

            StartCoroutine(Timer());
        }

        /// <summary>
        /// Set preparation timer before resuming game
        /// </summary>
        /// <returns></returns>
        private IEnumerator Timer()
        {
            for (var i = count; i >= 0; i--) {
                title.text = "Exercise resumed in " + i +  " second" + (i >= 2 ? "s" : "") + "...";
                tickSound.Play();

                yield return new WaitForSecondsRealtime(interval);
            }
            
            Unpause();
            ResetSheet();
        }
    }
}