using System.Collections;
using System.Collections.Generic;
using System;
using Common;
using Common.Audio;
using UnityEngine;

public class GhostManager : MonoBehaviour, ITargetable {

    public int timing = 3;
    public int killValue = 5;
    public float speed = 0.5f;

    public int minX = -10;
    public int minY = 0;
    public int minZ = -10;
    public int maxX = 10;
    public int maxY = 10;
    public int maxZ = 10;

    public AudioSource ghostSound;
    public AudioSource dieSound;
    public AudioSource sonarGhost;

    private IEnumerator kill;

    private Coroutine? _panicSoundFadeInCoroutine;
    private Coroutine? _panicSoundFadeOutCoroutine;

    public float smoothTime = 0.5F;
    private Vector3 velocity = Vector3.zero;

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

    public void OnEnter() {
        StartCoroutine(kill);

        if (_panicSoundFadeOutCoroutine != null)
        {
            StopCoroutine(_panicSoundFadeOutCoroutine);
        }

        _panicSoundFadeInCoroutine = StartCoroutine(AudioFadeOut.FadeIn(ghostSound, 0.3f));
    }

    public void OnExit() {
        StopCoroutine(kill);

        if (_panicSoundFadeInCoroutine != null)
        {
            StopCoroutine(_panicSoundFadeInCoroutine);
        }

        _panicSoundFadeOutCoroutine = StartCoroutine(AudioFadeOut.FadeOut(ghostSound, 0.1f));
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(timing);

        GameObject.Find("Environment").GetComponent<ScoreManager>().Add(killValue);

        ghostSound.Stop();
        AudioSource.PlayClipAtPoint(dieSound.clip, transform.position);

        Destroy(gameObject);
    }

    private void SonarTrigger()
    {
        StartCoroutine(Sonar());
    }

    private IEnumerator Sonar()
    {
        yield return new WaitForSeconds(Vector3.Distance(transform.position, GameObject.Find("Flashlight(Clone)").transform.position) / 2);

        sonarGhost.Play();

        yield return null;
    }
}