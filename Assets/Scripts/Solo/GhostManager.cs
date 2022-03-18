using System.Collections;
using System.Collections.Generic;
using System;
using Common;
using Common.Audio;
using UnityEngine;

/// <summary>
/// Class to describe ghost
/// </summary>
public class GhostManager : MonoBehaviour, ITargetable {

    /// <summary>
    /// Killing time in seconds
    /// </summary>
    public int timing = 3;

    /// <summary>
    /// Point gived by ghost at death
    /// </summary>
    public int killValue = 5;

    /// <summary>
    /// MinX value
    /// </summary>
    public int minX = -10;

    /// <summary>
    /// MinY value
    /// </summary>
    public int minY = 0;
    
    /// <summary>
    /// MinZ value
    /// </summary>
    public int minZ = -10;
    
    /// <summary>
    /// MaxX value
    /// </summary>
    public int maxX = 10;
    
    /// <summary>
    /// MaxY value
    /// </summary>
    public int maxY = 10;
    
    /// <summary>
    /// MaxZ value
    /// </summary>
    public int maxZ = 10;

    /// <summary>
    /// Ghost sound when target
    /// </summary>
    public AudioSource ghostSound;

    /// <summary>
    /// Ghost sound at death
    /// </summary>
    public AudioSource dieSound;

    /// <summary>
    /// Ghost response when sonar is used
    /// </summary>
    public AudioSource sonarGhost;
    
    // Call at death
    private IEnumerator kill;

    // Coroutine for panic sounds
    private Coroutine? _panicSoundFadeInCoroutine;
    private Coroutine? _panicSoundFadeOutCoroutine;

    // Ghost velocity
    private Vector3 velocity = Vector3.zero;

    // Target when ghost is moving
    private Vector3 target;

    void Start() {
        kill = Kill();

        GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.sonarTrigger.AddListener(SonarTrigger);

        target = transform.position;
    }

    void Update()
    {
        var playerCamera = Camera.main;
        if (playerCamera != null && Vector3.Distance(transform.position, target) < 0.1)
        {
            int x = UnityEngine.Random.Range(minX, maxX);
            int y = UnityEngine.Random.Range(minY, maxY);
            int z = UnityEngine.Random.Range(minZ, maxZ);

            target = new Vector3(x, y, x);
        }

        transform.LookAt(target);

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 3);

        transform.LookAt(playerCamera.transform);
        
    }

    /// <summary>
    /// Triggered when ghost is targeted
    /// </summary>
    public void OnEnter() {
        StartCoroutine(kill);

        if (_panicSoundFadeOutCoroutine != null)
        {
            StopCoroutine(_panicSoundFadeOutCoroutine);
        }

        _panicSoundFadeInCoroutine = StartCoroutine(AudioFadeOut.FadeIn(ghostSound, 0.3f));
    }

    /// <summary>
    /// Triggered when ghost leaves targeted state
    /// </summary>
    public void OnExit() {
        StopCoroutine(kill);

        if (_panicSoundFadeInCoroutine != null)
        {
            StopCoroutine(_panicSoundFadeInCoroutine);
        }

        _panicSoundFadeOutCoroutine = StartCoroutine(AudioFadeOut.FadeOut(ghostSound, 0.1f));
    }

    /// <summary>
    /// Function to manage ghost death
    /// </summary>
    /// <returns></returns>
    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(timing);

        GameObject.Find("Environment").GetComponent<ScoreManager>().Add(killValue);

        ghostSound.Stop();
        AudioSource.PlayClipAtPoint(dieSound.clip, transform.position);

        Destroy(gameObject);
    }
    
    /// <summary>
    /// Call when sonar is used
    /// </summary>
    private void SonarTrigger()
    {
        StartCoroutine(Sonar());
    }

    /// <summary>
    /// Sonar function playing the sound
    /// </summary>
    /// <returns></returns>
    private IEnumerator Sonar()
    {
        yield return new WaitForSeconds(Vector3.Distance(transform.position, GameObject.Find("Flashlight(Clone)").transform.position) / 2);

        sonarGhost.Play();

        yield return null;
    }
}