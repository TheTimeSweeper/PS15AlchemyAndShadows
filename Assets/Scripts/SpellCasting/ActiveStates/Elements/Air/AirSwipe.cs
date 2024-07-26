namespace ActiveStates.Elements.Air
{
    public class AirSwipe : ElementThrowProjectile
    {
        public override void OnEnter()
        {
            base.OnEnter();
            elementMass.SubMasses[0].transform.position = characterBody.transform.position;
        }
    }
}