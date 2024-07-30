using SpellCasting;

namespace ActiveStates.Elements.Earth
{
    public class EarthSummon : ElementSpawnEnemy
    {
        protected override CharacterBody minionPrefab => ((ElementTypeEnemyEarth)elementType).MinionPrefab;
        protected override int amount => 3;
    }
}