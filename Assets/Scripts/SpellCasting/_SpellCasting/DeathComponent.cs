using ActiveStates;
using SpellCasting;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    [SerializeField]
    private SpawnTable deathTable;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private StateMachineLocator stateMachineLocator;

    public void GetRektLol()
    {
        if (deathTable)
        {
            deathTable.SpawnObject(transform.position);
        }
        if (deathEffect)
        {
            Object.Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        if (stateMachineLocator)
        {
            stateMachineLocator.SetStates(new IdleState());
        }

        Object.Destroy(gameObject);
    }

}