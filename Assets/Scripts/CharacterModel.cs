using System;
using System.Collections.Generic;
using UnityEngine;
namespace SpellCasting
{
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField]
        private Renderer[] elementColoredRenderers;

        [SerializeField]
        private Color noElementColor = Color.gray;

        private MaterialPropertyBlock[] materialBlocks;

        private List<Color> currentColors = new List<Color>();

        private void Awake()
        {
            materialBlocks = new MaterialPropertyBlock[elementColoredRenderers.Length];
            for (int i = 0; i < elementColoredRenderers.Length; i++) {
                materialBlocks[i] = new MaterialPropertyBlock();
                elementColoredRenderers[i].GetPropertyBlock(materialBlocks[i]);
            }
            UpdateElementColors();
        }

        public void UpdateElementColors()
        {
            Color currentColor = currentColors.Count > 0 ? currentColors[0] : noElementColor;

            for (int i = 0; i < elementColoredRenderers.Length && i < materialBlocks.Length; i++)
            {
                materialBlocks[i].SetColor("_Color", currentColor);
                elementColoredRenderers[i].SetPropertyBlock(materialBlocks[i]);
            }
        }
        public void SetElementColor(Color newColor)
        {
            currentColors.Insert(0, newColor);
            UpdateElementColors();
        }
        public void RemoveElementColor(Color removingColor)
        {
            for (int i = 0; i < currentColors.Count; i++)
            {
                if(currentColors[i] == removingColor)
                {
                    currentColors.RemoveAt(i);
                    break;
                }
            }
            UpdateElementColors();
        }
    }
}