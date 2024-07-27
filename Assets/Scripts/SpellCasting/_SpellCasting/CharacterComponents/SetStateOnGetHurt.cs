using ActiveStates;
using ActiveStates.Characters;
using UnityEngine;

namespace SpellCasting
{
    [RequireComponent(typeof(CharacterBody))]
    public class SetStateOnGetHurt : MonoBehaviour
    {
        [SerializeField]
        private CommonComponentsHolder commonComponents;

        [SerializeField]
        private float hitStunTime;
        [SerializeField]
        private float hitStunThreshold;
        [SerializeField]
        private float hitMoveThreshold;

        private float stunFactor
        {
            get
            {
                if(commonComponents?.CharacterBody!= null)
                {
                    return commonComponents.CharacterBody.stats.StunFactor;
                }
                return 1;
            }
        }

        private float knockbackFactor
        {
            get
            {
                if (commonComponents?.CharacterBody != null)
                {
                    return commonComponents.CharacterBody.stats.KnockbackFactor;
                }
                return 1;
            }
        }

        private void Reset()
        {
            commonComponents = GetComponent<CommonComponentsHolder>();
        }

        private void Awake()
        {
            commonComponents.HealthComponent.OnDamageTaken += HealthComponent_OnDamageTaken; ;
        }

        private void HealthComponent_OnDamageTaken(GetDamagedinfo damagedInfo)
        {
            if (damagedInfo.DamagingInfo.DamageValue > hitStunThreshold)
            {
                commonComponents.StateMachineLocator.SetStates(new StunnedState { StunTime = hitStunTime * stunFactor }, InterruptPriority.HITSTUN);
            }
            else if (damagedInfo.DamagingInfo.DamageValue > hitMoveThreshold)
            {
                commonComponents.StateMachineLocator.SetStates(new StunnedState { StunTime = hitStunTime * stunFactor }, InterruptPriority.HITSTUN, true, false);
            }

        }
    }
}