using NUnit.Framework;

namespace SpellCasting
{
    public class DamageTypeCatalog : Catalog<DamageTypeCatalog, DamageTypeIndex, DamageTypeInfo>
    {
        protected override DamageTypeIndex GetIndexFromItem(DamageTypeInfo item)
        {
            return item.damageTypeIndex;
        }

        public static void PreModifyDamageAll(GetDamagedinfo info)
        {
            foreach (DamageTypeIndex item in catalogIndexMap.Keys)
            {
                if (info.DamagingInfo.DamageTypeIndex.HasFlag(item))
                {
                    catalogIndexMap[item].PreModifyDamage(info);
                }
            }
        }

        public static void OnTakeDamageAll(GetDamagedinfo info)
        {
            foreach (DamageTypeIndex item in catalogIndexMap.Keys)
            {
                if (info.DamagingInfo.DamageTypeIndex.HasFlag(item))
                {
                    catalogIndexMap[item].OnTakeDamage(info);
                }
            }
        }
    }
}