using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpellCasting.UI
{
    public class DebugMenu : MonoBehaviour
    {
        [SerializeField]
        private Button jango;
        [SerializeField]
        private Toggle jangoToggle;

        [SerializeField]
        private Slider timeScaleSlider;
        [SerializeField]
        private TMP_Text timeScaleText;

        private void Start()
        {
            timeScaleSlider.onValueChanged.AddListener(OnTimeScaleSliderChanged);

            timeScaleSlider.value = getInverseTimeScaleSliderValue();
            timeScaleText.text = $"Time Scale: {TimeStopperInstance.UnpausedTime.ToString("0.00")}";

            foreach (ElementType element in ElementCatalog.Instance.ElementTypes.Values)
            {
                AddToggleElementButton(element);
            }
        }

        private void AddToggleElementButton(ElementType value)
        {
            Toggle newToggle = Instantiate(jangoToggle, jangoToggle.transform.parent);
            newToggle.gameObject.SetActive(true);

            newToggle.GetComponentInChildren<TMPro.TMP_Text>().text = $"unlock {value.name}";

            bool elementUnlocked = MainGame.Instance.SavedData.UnlockedElements.Contains(value);
            newToggle.isOn = elementUnlocked;

            newToggle.onValueChanged.AddListener(ToggleElement);

            void ToggleElement(bool toggled)
            {
                if (toggled)
                {
                    MainGame.Instance.SavedData.AddElement(value);
                } 
                else
                {
                    MainGame.Instance.SavedData.RemoveElement(value);
                }
            }
        }

        private float getInverseTimeScaleSliderValue()
        {
            if (TimeStopperInstance.UnpausedTime < 1)
            {
                return Mathf.Lerp(0, 0.25f, Mathf.InverseLerp(0, 1, TimeStopperInstance.UnpausedTime));
            }
            else if (TimeStopperInstance.UnpausedTime < 5)
            {
                return Mathf.Lerp(0.25f, 0.64f, Mathf.InverseLerp(1, 5, TimeStopperInstance.UnpausedTime));
            }
            else
            {
                return Mathf.Lerp(0.64f, 1, Mathf.InverseLerp(5, 50, TimeStopperInstance.UnpausedTime));
            }
        }

        public void OnTimeScaleSliderChanged(float sliderValue)
        {
            //split bar into 3 sections for time 0-1, 1-5, 5-50
            if (sliderValue < 0.25f) {

                sliderValue = Mathf.Lerp(0, 1, Mathf.InverseLerp(0f, 0.25f, sliderValue));
            } else if (sliderValue < 0.64f) {

                sliderValue = Mathf.Lerp(1, 5, Mathf.InverseLerp(0.25f, 0.64f, sliderValue));
            } else {

                sliderValue = Mathf.Lerp(5, 50, Mathf.InverseLerp(0.64f, 1f, sliderValue));
            }

            TimeStopperInstance.UnpausedTime = sliderValue;

            timeScaleText.text = $"Time Scale: {sliderValue.ToString("0.00")}";
        }
    }
}