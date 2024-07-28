using UnityEngine;
namespace SpellCasting
{
    [CreateAssetMenu(menuName = "SpellCasting/BuffInfo/Default", fileName = "Buff")]
    public class BuffInfo : ScriptableObject
    {
        [SerializeField]
        private string buffID;

        [SerializeField]
        private EffectPooled buffEffectPrefab;

        [SerializeField]
        private Sprite icon;

        [SerializeField, Tooltip("currently only maxhealth, damage, basemovespeed")]
        public CharacterStats statsToChange;

        public virtual void OnApply(CharacterBody body)
        {
            if(statsToChange.BaseMaxHealth > 0)
            {
                body.stats.MaxHealth.ApplyMultiplyModifier(statsToChange.BaseMaxHealth, buffID);
            }

            if (statsToChange.BaseDamage > 0)
            {
                body.stats.Damage.ApplyMultiplyModifier(statsToChange.BaseDamage, buffID);
            }

            if (statsToChange.BaseMoveSpeed > 0)
            {
                body.stats.MoveSpeed.ApplyMultiplyModifier(statsToChange.BaseMoveSpeed, buffID);
            }

            if(buffEffectPrefab != null)
            {
                EffectManager.SpawnEffect(buffEffectPrefab.effectIndex, body.transform.position, body.transform);
            }

            //jam and so on and so forth
        }

        public virtual void OnUnapply(CharacterBody body)
        {
            if (statsToChange.BaseMaxHealth > 0)
            {
                body.stats.MaxHealth.RemoveModifier(buffID);
            }

            if (statsToChange.BaseDamage > 0)
            {
                body.stats.Damage.RemoveModifier(buffID);
            }

            if (statsToChange.BaseMoveSpeed > 0)
            {
                body.stats.MoveSpeed.RemoveModifier(buffID);
            }
        }
    }
}