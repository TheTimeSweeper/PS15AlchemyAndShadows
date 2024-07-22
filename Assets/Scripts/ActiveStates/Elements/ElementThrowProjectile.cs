namespace ActiveStates.Elements
{
    public class ElementThrowProjectile : BasicElementThrow
    {
        public override void OnEnter()
        {
            base.OnEnter();

            for (int i = 0; i < elementMass.SubMasses.Count; i++)
            {
                elementMass.SubMasses[i].JAMActivateProjectile(characterBody);
            }
        }
    }
}
