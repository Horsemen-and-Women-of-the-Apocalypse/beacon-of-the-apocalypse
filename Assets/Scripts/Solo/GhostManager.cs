using System.Collections;
using System.Collections.Generic;
using System;
using Common;
using UnityEngine;

public class GhostManager : MonoBehaviour, ITargetable {
    public int timing = 3;
    public int killValue = 5;
    public float speed = 0.5f;

    public int minX = -5, minY = 0, minZ = -5;
    public int maxX = 5, maxY = 5, maxZ = 5;

    private IEnumerator kill;
    private IEnumerator move;

    void Start() {
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
            yield return new WaitForSeconds(3);

            float xBase = transform.position.x;
            float yBase = transform.position.y;
            float zBase = transform.position.z;

            System.Random random = new System.Random();

            float x = random.Next(minX, maxX);
            float y = random.Next(minY, maxY);
            float z = random.Next(minZ, maxZ);
            
            transform.position += new Vector3(x, y, z);

            var playerCamera = Camera.main;
            if (playerCamera != null)
            {
                transform.LookAt(playerCamera.transform);
            }
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