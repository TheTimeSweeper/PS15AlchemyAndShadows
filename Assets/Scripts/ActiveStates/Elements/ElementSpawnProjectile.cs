using SpellCasting.Projectiles;
using UnityEngine;

namespace ActiveStates.Elements
{
    public abstract class ElementSpawnProjectile : BaseElementMassState
    {
        protected abstract ProjectileController prefab { get; }
        public override void OnEnter()
        {
            base.OnEnter();

            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                ProjectileController explosionProjectile = Object.Instantiate(prefab, elementMass.SubMasses[i].transform.position, Quaternion.identity);
                explosionProjectile.Init(characterBody);
            }

            elementMass.Fizzle();
        }
    }
}
