using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Class to spawn a power-up
/// </summary>
public class SpawnPowerUp : MonoBehaviour
{
    /// <summary>
    /// List of power-up
    /// </summary>
    public List<GameObject> powerUps;

    /// <summary>
    /// Luck for items loot (50 = 50%)
    /// </summary>
    public float luck = 50;

    // Random number generator
    private static RandomNumberGenerator random = RandomNumberGenerator.Create();

    void OnDestroy()
    {
        int loot = getRandom(0, 100);

        if(luck > loot)
        {
            int choice = getRandom(0, powerUps.Count - 1);
            GameObject spawnablePowerUp = powerUps[choice];

            var instance = Instantiate(spawnablePowerUp, transform);

            GameObject items = GameObject.Find("Items");
            
            // TODO: Look at camera

            if (items != null)
            {
                instance.transform.SetParent(items.transform);
            }
        } 
    }

    /// <summary>
    /// Return random number between min and max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    int getRandom(int min, int max)
    {
        var bytes = new byte[sizeof(int)];
        random.GetNonZeroBytes(bytes);
        var val = BitConverter.ToInt32(bytes, 0);

        return ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;
    }
}