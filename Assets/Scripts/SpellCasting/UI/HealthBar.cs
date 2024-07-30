using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpellCasting.UI
{

    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider healthSlider;

        [SerializeField]
        private Image delayedSlider;
        [SerializeField]
        private TMP_Text healthText;

        private HealthComponent _healthComponent;

        public void Init(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        private void Update()
        {
            if ((_healthComponent == null))
                return;

            if (healthSlider != null)
            {
                healthSlider.value = _healthComponent.Health / _healthComponent.MaxHealth;

                if (delayedSlider != null)
                {
                    delayedSlider.fillAmount = Util.ExpDecayLerp(delayedSlider.fillAmount, healthSlider.value, 3, Time.deltaTime);
                }
            }
            if(healthText != null)
            {
                healthText.text = _healthComponent.Health.ToString("0");
            }
        }

    }
}