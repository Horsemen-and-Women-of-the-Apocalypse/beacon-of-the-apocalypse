using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{

    private static RandomNumberGenerator random = RandomNumberGenerator.Create();

    public List<GameObject> powerUps;

    // Keep to test
    void Start()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        int choice = getRandom(0, powerUps.Count - 1);
        GameObject spawnablePowerUp = powerUps[choice];

        // TODO fix position + parent empty power up object (to store all power ups)
        var instance = Instantiate(spawnablePowerUp, transform);
        instance.transform.SetParent(null);
    }

    int getRandom(int min, int max)
    {
        var bytes = new byte[sizeof(int)];
        random.GetNonZeroBytes(bytes);
        var val = BitConverter.ToInt32(bytes, 0);

        return ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;
    }
}
