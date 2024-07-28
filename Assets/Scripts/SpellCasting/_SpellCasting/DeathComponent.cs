using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    [SerializeField]
    private RewardTable deathTable;

    [SerializeField]
    private GameObject deathEffect;

    public void GetRektLol()
    {
        if (deathTable)
        {
            deathTable.SpawnReward(transform.position);
        }
        if (deathEffect)
        {
            Object.Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Object.Destroy(gameObject);
    }

}