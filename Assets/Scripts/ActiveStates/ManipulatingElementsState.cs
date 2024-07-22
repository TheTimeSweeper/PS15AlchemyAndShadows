
using SpellCasting;
using System.Collections.Generic;

namespace ActiveStates
{
    public class ManipulatingElementsState : ActiveState
    {
        public List<ElementManipulation> _elementManipulations = new List<ElementManipulation>();

        public override void OnEnter()
        {
            base.OnEnter();
            _elementManipulations = caster.ElementManipulations;
        }

        public override void OnUpdate()
        {
            for (int i = 0; i < _elementManipulations.Count; i++)
            {
                _elementManipulations[i].Update(machine.CommonComponents);
            }
        }

        public override void OnFixedUpdate()
        {
            for (int i = 0; i < _elementManipulations.Count; i++)
            {
                _elementManipulations[i].FixedUpdate(inputBank);
            }
        }
    }
}
