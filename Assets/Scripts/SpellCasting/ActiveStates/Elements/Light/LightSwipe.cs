namespace ActiveStates.Elements.Light
{
    public class LightSwipe : BaseElementMassState
    {
        public override void OnEnter()
        {
            base.OnEnter();

            if (animator != null)
            {
                //animator.Update(0f);
                animator.PlayInFixedTime("Cast", -1, 0);
            }

            healthComponent.Heal(new SpellCasting.HealingInfo { HealValue = 0.3f * characterBody.stats.MaxHealth});
            elementMass.Fizzle();
        }
    }
}