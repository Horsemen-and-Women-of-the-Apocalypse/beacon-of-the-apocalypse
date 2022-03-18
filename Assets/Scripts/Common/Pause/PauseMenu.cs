using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

namespace Common.Pause
{
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// Flag to get pause state
        /// </summary>
        public static bool isGamePaused = false;

        public GameObject pauseMenuUI;

        public SceneChanger sceneChanger;
        
        /// <summary>
        /// Switch on/off existing camera light
        /// Otherwise, enemies are still visible
        /// </summary>
        public Light cameraLight = null;

        public Flashlight flashlight = null;

        public Sheet sheet;

        public XRInteractorLineVisual XRLine;

        private Material _skybox;

        private AudioSource[] _sounds;

        private const string BUTTON_SOUND = "Button sound";
        private const float BUTTON_SOUND_PROB = 0.85f;
        private const string BUTTON_CURSED_SOUND = "Cursed sound";

        private void Start()
        {
            _skybox = RenderSettings.skybox;
            EnableSounds();
        }

        /// <summary>
        /// Method to be given to XRRig OnButtonLongPressed event
        /// </summary>
        public void TriggerPause()
        {
            if (isGamePaused)
                Resume();
            else
                Pause();
        }

        private void TurnOnOffCameraLight()
        {
            if (cameraLight) cameraLight.enabled = !cameraLight.enabled;
        }

        private void TurnOnOffFlashlight()
        {
            // TODO: Prevent player switching on/off
            if (!flashlight)
            {
                GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.TurnOnOff();
                return;
            }
            flashlight.TurnOnOff();
        }

        private void TurnOnOffXRLine()
        {
            XRLine.enabled = !XRLine.enabled;
        }

        /// <summary>
        /// Allow menu sounds to be played despite pause state
        /// </summary>
        private void EnableSounds()
        {
            _sounds = GetComponentsInChildren<AudioSource>();
            foreach (var sound in _sounds)
            {
                sound.ignoreListenerPause = true;
            }
        }

        /// <summary>
        /// Get audio source by name
        /// </summary>
        /// <param name="soundName">Audio source name (as defined on hierarchy)</param>
        /// <returns>Audio source</returns>
        private AudioSource GetSound(string soundName)
        {
            foreach (var sound in _sounds)
            {
                if (sound.name == soundName) return sound;
            }
            return null;
        }

        /// <summary>
        /// Play random sound
        /// </summary>
        protected void PlayButtonSound()
        {
            GetSound(Random.value < BUTTON_SOUND_PROB ? BUTTON_SOUND : BUTTON_CURSED_SOUND).Play();
        }
        
        /// <summary>
        /// Disable pause and resume game
        /// </summary>
        public virtual void Resume()
        {
            PlayButtonSound();

            Unpause();
        }

        /// <summary>
        /// Common unpause behavior
        /// </summary>
        protected void Unpause()
        {
            sheet.StartFollowing();
            AudioListener.pause = false;
            
            TurnOnOffXRLine();
            
            // Enable game elements
            TurnOnOffCameraLight();
            TurnOnOffFlashlight();
            
            RenderSettings.skybox = _skybox;
            Time.timeScale = 1f;
            
            pauseMenuUI.SetActive(false);
            isGamePaused = false;
        }
        
        /// <summary>
        /// Pause game
        /// </summary>
        void Pause()
        {
            AudioListener.pause = true; // PauseBackgroundMusic();
            // TODO: Remove all ghosts from map
            TurnOnOffCameraLight();
            TurnOnOffFlashlight();
            TurnOnOffXRLine();
            RenderSettings.skybox = (null);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            sheet.StartFollowing();
            isGamePaused = true;
        }

        /// <summary>
        /// Load Menu scene
        /// </summary>
        public void LoadMenu()
        {
            PlayButtonSound();
            isGamePaused = false;
            pauseMenuUI.SetActive(false);
            sceneChanger.Menu();
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public void Exit()
        {
            PlayButtonSound();
            pauseMenuUI.SetActive(false);
            sceneChanger.ExitGame();
        }
    }
}
