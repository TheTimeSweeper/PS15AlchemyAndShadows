using ActiveStates;
using SpellCasting;
using System.Collections.Generic;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    //jam uh shouldn't be here lol
    public static List<DeathComponent> Players = new List<DeathComponent>();

    private void OnEnable()
    {
        if (gameObject.CompareTag("Player"))
        {
            Players.Add(this);
        }

    }
    private void OnDestroy()
    {
        if (gameObject.CompareTag("Player"))
        {
            Players.Remove(this);

            if(Players.Count <= 0)
            {
                SpellCasting.UI.ConfirmPopup.Open("Game Over!", "return", "", () =>
                {
                    LevelProgressionManager.TrueReset();
                });
            }
        }
    }

    [SerializeField]
    private SpawnTable deathTable;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private EffectPooled deathEffectPooled;

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
        if (deathEffectPooled)
        {
            EffectManager.SpawnEffect(deathEffectPooled.effectIndex, transform.position);
        }

        if (stateMachineLocator)
        {
            stateMachineLocator.SetStates(new IdleState());
        }

        Object.Destroy(gameObject);
    }

}