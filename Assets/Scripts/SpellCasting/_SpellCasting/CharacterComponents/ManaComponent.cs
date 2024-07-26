using UnityEngine;
namespace SpellCasting
{
    public class ManaComponent : MonoBehaviour, IHasCommonComponents
    {
        public CommonComponentsHolder CommonComponents => throw new System.NotImplementedException();
    }
}