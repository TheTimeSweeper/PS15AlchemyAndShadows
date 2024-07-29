﻿
using SpellCasting.World;
using SpellCasting;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Linq;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "SpellCasting/SpawnTable/Default", fileName = "Table")]
public class SpawnTable : ScriptableObject
{
    [SerializeField]
    protected List<GameObject> spawnItems;

    [SerializeField]
    protected List<float> weights;

    public virtual void SpawnObject(Vector3 position)
    {
        int randomIndex = Util.WeightedRandomIndex(GetWeights());
        if (randomIndex == -1)
            return;

        List<GameObject> Table = GetTable();

        GameObject item = Table[randomIndex];

        Object.Instantiate(item, position, Quaternion.identity);
    }

    protected virtual List<GameObject> GetTable()
    {
        return spawnItems;
    }

    protected virtual List<float> GetWeights()
    {
        return weights;
    }

    protected virtual void OnValidate()
    {
        if(weights.Count < spawnItems.Count)
        {
            weights.Add(0);
        }    
        for (int i = weights.Count - 1; i >= 0 && weights.Count > spawnItems.Count; i--)
        {
            weights.RemoveAt(i);
        }
    }
}