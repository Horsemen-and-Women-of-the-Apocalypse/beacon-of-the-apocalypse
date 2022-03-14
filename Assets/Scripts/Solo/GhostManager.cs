using System.Collections;
using System.Collections.Generic;
using System;
using Common;
using UnityEngine;

public class GhostManager : MonoBehaviour, ITargetable {

    public int timing = 3;
    public int killValue = 5;
    public float speed = 10f;

    public float minX, minY, minZ = 0f;
    public float maxX, maxY, maxZ = 100f;

    private IEnumerator kill;
    private IEnumerator move;

    void Start()
    {
        kill = Kill();
        move = Move();

        StartCoroutine(move);
    }

    public void OnEnter() {
        StartCoroutine(kill);
    }

    public void OnExit() {
        StopCoroutine(kill);
    }

    void OnDestroy()
    {
        StopCoroutine(move);
    }

    private IEnumerator Move()
    {
        while(true)
        {
            float xBase = transform.position.x;
            float yBase = transform.position.y;
            float zBase = transform.position.z;

            System.Random random = new System.Random();

            int x = random.Next(-50, 50);
            int y = random.Next(-50, 50);
            int z = random.Next(-50, 50);

            float newX = x * speed * Time.deltaTime > minX && x * speed * Time.deltaTime < maxX ? x * speed * Time.deltaTime : xBase;
            float newY = y * speed * Time.deltaTime > minY && y * speed * Time.deltaTime < maxY ? y * speed * Time.deltaTime : yBase;
            float newZ = z * speed * Time.deltaTime > minZ && x * speed * Time.deltaTime < maxZ ? z * speed * Time.deltaTime : zBase;

            transform.position = transform.position + new Vector3(newX, newY, 0);

            yield return new WaitForSeconds(0.5f);
        }   

        yield return null;
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(timing);

        GameObject.Find("Environment").GetComponent<ScoreManager>().Add(killValue);

        Destroy(gameObject);
    }
}
