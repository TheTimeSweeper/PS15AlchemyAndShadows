using UnityEngine;
namespace SpellCasting
{
    //jam I should not be doing this
    public class EffectPooled : MonoBehaviour
    {
        [SerializeField]
        public EffectIndex effectIndex;

        [SerializeField]
        private ParticleSystem[] systems;

        [SerializeField]
        private float effectTime;

        private float _tim;

        void Update()
        {
            _tim += Time.deltaTime;
            if(_tim > effectTime)
            {
                EffectManager.ReturnEffect(this);
                _tim = 0;
            }
        }

        public EffectPooled StartEffect(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].Play();
            }
            return this;
        }

        public EffectPooled EndEffect()
        {
            gameObject.SetActive(false);
            for (int i = 0; i < systems.Length; i++)
            {
                systems[i].Stop();
            }
            return this;
        }
    }
}