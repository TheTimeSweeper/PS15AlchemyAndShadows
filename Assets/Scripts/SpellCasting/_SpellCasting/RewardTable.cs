
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellCasting/RewardTable", fileName = "RewardTable")]
public class RewardTable : ScriptableObject
{
    [SerializeField]
    private List<GameObject> rewardItems;

    [SerializeField]
    private List<float> weights;

    public void SpawnReward(Vector3 position)
    {
        int randomIndex = Util.WeightedRandomIndex(weights);
        if (randomIndex == -1)
            return;

        GameObject item = rewardItems[randomIndex];

        Object.Instantiate(item, position, Quaternion.identity);
    }

    void OnValidate()
    {
        if(weights.Count < rewardItems.Count)
        {
            weights.Add(0);
        }    
        for (int i = weights.Count - 1; i >= 0 && weights.Count > rewardItems.Count; i--)
        {
            weights.RemoveAt(i);
        }
    }
}