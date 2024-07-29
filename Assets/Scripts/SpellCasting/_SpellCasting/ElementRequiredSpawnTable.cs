using SpellCasting;
using System.Collections.Generic;
using UnityEngine;

//jam should be a scriptableobject?
[System.Serializable]
public class ElementRequriedSpawn
{
    public ElementType RequiredElement;
    public List<GameObject> spawnItems;
    public List<float> weights;

    public void OnValidate()
    {
        if (weights.Count < spawnItems.Count)
        {
            weights.Add(0);
        }
        for (int i = weights.Count - 1; i >= 0 && weights.Count > spawnItems.Count; i--)
        {
            weights.RemoveAt(i);
        }
    }
}

[CreateAssetMenu(menuName = "SpellCasting/SpawnTable/ElementsRequried", fileName = "Table")]
public class ElementRequiredSpawnTable : SpawnTable
{
    [SerializeField, Space]
    private List<ElementRequriedSpawn> elementRequiredItems;

    protected override List<GameObject> GetTable()
    {
        List<GameObject> newList = new List<GameObject>();
        newList.AddRange(spawnItems);

        for (int i = 0; i < elementRequiredItems.Count; i++)
        {
            if (MainGame.Instance.SavedData.UnlockedElements.Contains(elementRequiredItems[i].RequiredElement))
            {
                newList.AddRange(elementRequiredItems[i].spawnItems);
            }
        }

        return newList;
    }

    protected override List<float> GetWeights()
    {
        List<float> newList = new List<float>();
        newList.AddRange(weights);

        for (int i = 0; i < elementRequiredItems.Count; i++)
        {
            if (MainGame.Instance.SavedData.UnlockedElements.Contains(elementRequiredItems[i].RequiredElement))
            {
                newList.AddRange(elementRequiredItems[i].weights);
            }
        }
        return newList;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        for (int i = 0; i < elementRequiredItems.Count; i++)
        {
            elementRequiredItems[i].OnValidate();
        }
    }
}
