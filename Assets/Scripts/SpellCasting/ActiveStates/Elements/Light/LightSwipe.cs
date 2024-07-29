namespace ActiveStates.Elements.Light
{
    public class LightSwipe : BaseElementMassState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            healthComponent.Heal(new SpellCasting.HealingInfo { HealValue = 0.3f * characterBody.stats.MaxHealth});
            elementMass.Fizzle();
        }
    }
}