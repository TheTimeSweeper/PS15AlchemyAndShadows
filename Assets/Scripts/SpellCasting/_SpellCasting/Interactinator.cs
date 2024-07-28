using SpellCasting;
using UnityEngine;

public class Interactinator : MonoBehaviour
{
    [SerializeField]
    private InteractionHandler interactionHandler;

    private float _timer = 0.2f;

    private CharacterBody Body;

    private void FixedUpdate()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _timer = 0.2f;
            CheckInteractable();
        }
        
        if (Body != null && Body.CommonComponents.InputBank.E.JustPressed(this))
        {
            interactionHandler.HandleInteraction(Body);
        }
    }

    private void CheckInteractable()
    {
        Body = CharacterBodyTracker.FindBodyByDistance(TeamIndex.PLAYER, transform.position, 10);
    }
}
