﻿using SpellCasting;

namespace ActiveStates.Elements.Earth
{

    public class EarthSwipe : ElementThrowProjectile
    {
        protected override DamageTypeIndex getDamageType()
        {
            return DamageTypeIndex.EARTH | DamageTypeIndex.STUN;
        }
    }
}