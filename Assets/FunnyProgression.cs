using UnityEngine;

public class FunnyProgression : MonoBehaviour
{
    [SerializeField]
    private Material[] progressionmaterials;


    [SerializeField]
    private Renderer leRenderer;

    private void Reset()
    {
        leRenderer  = GetComponent<Renderer>();
    }

    void Start()
    {
        if (progressionmaterials == null || progressionmaterials.Length == 0)
            return;

        leRenderer.sharedMaterial = progressionmaterials[SpellCasting.LevelProgressionManager.DifficultyProgression % progressionmaterials.Length];
    }
}
