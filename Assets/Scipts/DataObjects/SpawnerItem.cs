using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnerItem
{
    [SerializeField]
    public GameObject item;
    [SerializeField]
    public float spawnWeight;

    public static GameObject ChooseItemFromList(IEnumerable<SpawnerItem> items)
    {
        float totalWeight = 0f;
        foreach (var item in items)
        {
            totalWeight += item.spawnWeight;
        }
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        foreach (var item in items)
        {
            cumulativeWeight += item.spawnWeight;
            if (randomValue <= cumulativeWeight)
            {
                return item.item;
            }
        }
        return items.First().item; // Fallback, should not reach here if weights are set correctly
    }
}