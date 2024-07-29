using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpellCasting.UI
{
    public class ElementHud : MonoBehaviour
    {
        [SerializeField]
        private Image[] ImagesOfColor;

        [SerializeField]
        private Image ElementIcon;

        [SerializeField]
        private TMP_Text ManaText;
        [SerializeField]
        private Slider ManaSlider;
        [SerializeField]
        private Image DelayedSlider;

        private CommonComponentsHolder _referenceComponents;
        private InputState _inputState;

        private ElementType _currentElement;
        private ElementType currentElement
        {
            get
            {
                CheckCurrentElement();
                return _currentElement;
            }
        }

        private void CheckCurrentElement()
        {
            //wait it's not gonna change. idk how I'd do overrides
            ElementType checkElement = null;
            if (_referenceComponents != null && _referenceComponents.Caster != null)
            {
                checkElement = _referenceComponents.Caster.InputToElement[_inputState];
            }

            _currentElement = checkElement;

            if (checkElement != null)
            {
                BindElementUI();
            }
        }

        private void BindElementUI()
        {
            for (int i = 0; i < ImagesOfColor.Length; i++)
            {
                ImagesOfColor[i].color = _currentElement.ElementColor;
            }
            ElementIcon.sprite = _currentElement.Icon;
        }

        public void Init(CommonComponentsHolder referenceCommonComponents, InputState inputState)
        {
            _referenceComponents = referenceCommonComponents;
            _inputState = inputState;
            CheckCurrentElement();
        }

        private void Update()
        {
            if (_referenceComponents == null)
                return;

            if(currentElement == null)
            {
                CheckCurrentElement();
            }
            if(currentElement == null)
                return;

            switch (_currentElement.Index)
            {
                case ElementTypeIndex.FIRE:
                    ManaText.text = _referenceComponents.ManaComponent.FireMana.ToString("0");
                    ManaSlider.value = _referenceComponents.ManaComponent.FireMana/ _referenceComponents.CharacterBody.stats.MaxFireMana;
                    break;
                case ElementTypeIndex.EARTH:
                    ManaText.text = _referenceComponents.ManaComponent.EarthMana.ToString("0");
                    ManaSlider.value = _referenceComponents.ManaComponent.EarthMana / _referenceComponents.CharacterBody.stats.MaxEarthMana;
                    break;
                case ElementTypeIndex.AIR:
                    ManaText.text = _referenceComponents.ManaComponent.AirMana.ToString("0");
                    ManaSlider.value = _referenceComponents.ManaComponent.AirMana / _referenceComponents.CharacterBody.stats.MaxAirMana;
                    break;
                case ElementTypeIndex.WATER:
                    ManaText.text = _referenceComponents.ManaComponent.WaterMana.ToString("0");
                    ManaSlider.value = _referenceComponents.ManaComponent.WaterMana / _referenceComponents.CharacterBody.stats.MaxWaterMana;
                    break;
            }

            DelayedSlider.fillAmount = Util.ExpDecayLerp(DelayedSlider.fillAmount, ManaSlider.value, 3, Time.deltaTime);
        }

    }
}
