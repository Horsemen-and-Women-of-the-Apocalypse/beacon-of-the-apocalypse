using System.Collections;
using Common;
using UnityEngine;

public class GhostManager : MonoBehaviour, ITargetable {
    public int timing = 3;

    public int killValue = 5;

    private IEnumerator kill;

    void Start() {
        kill = Kill();
    }

    public void OnEnter() {
        StartCoroutine(kill);
    }

    public void OnExit() {
        StopCoroutine(kill);
    }

    private IEnumerator Kill() {
        yield return new WaitForSeconds(timing);

        GameObject.Find("Environment").GetComponent<ScoreManager>().Add(killValue);

        Destroy(gameObject);
    }
}