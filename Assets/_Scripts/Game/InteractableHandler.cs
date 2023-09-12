using UnityEngine;

public class InteractableHandler 
{
    private SoundInteractableFactory SoundInteractableFactory => GameContext.Instance.SoundInteractableFactory;
    private SoundPlayer SoundPlayer => GameContext.Instance.SoundPlayer;

    public void InteractablesEnter(IInteractable interactable1, IInteractable interactable2)
    {
        if (interactable1 is IPhysicsInteractable && interactable2 is IPhysicsInteractable)
        {
            IPhysicsInteractable physicsInteractable1 = interactable1 as IPhysicsInteractable;
            IPhysicsInteractable physicsInteractable2 = interactable2 as IPhysicsInteractable;

            if (physicsInteractable1.Rigidbody.velocity.magnitude * physicsInteractable1.Strength 
                > physicsInteractable2.Rigidbody.velocity.magnitude * physicsInteractable2.Strength)
            {
                TryPlayInteractionSound(physicsInteractable1, physicsInteractable2, 
                    physicsInteractable1.Rigidbody.velocity.magnitude * physicsInteractable1.Strength);
            }
        }
        else if(interactable1 is IPhysicsInteractable) 
        {
            IPhysicsInteractable physicsInteractable = interactable1 as IPhysicsInteractable;
            TryPlayInteractionSound(interactable1, interactable2, physicsInteractable.Rigidbody.velocity.magnitude * physicsInteractable.Strength);
        }
        else if(interactable2 is IPhysicsInteractable)
        {
            IPhysicsInteractable physicsInteractable = interactable2 as IPhysicsInteractable;
            TryPlayInteractionSound(interactable1, interactable2, physicsInteractable.Rigidbody.velocity.magnitude * physicsInteractable.Strength);
        }
    }

    public void InteractablesStay(IInteractable interactable1, IInteractable interactable2)
    {
        if(interactable1 is IPhysicsInteractable)
        {
            IPhysicsInteractable physicsInteractable = interactable1 as IPhysicsInteractable;
            SoundPlayer.ChangeVolume(physicsInteractable.Rigidbody.velocity.magnitude * physicsInteractable.Strength / 100);
        }
        else if(interactable2 is IPhysicsInteractable)
        {
            IPhysicsInteractable physicsInteractable = interactable2 as IPhysicsInteractable;
            SoundPlayer.ChangeVolume(physicsInteractable.Rigidbody.velocity.magnitude * physicsInteractable.Strength / 100);
        }
    }

    public void InteractablesExit(IInteractable interactable1, IInteractable interactable2)
    {
        SoundPlayer.StopSound();
    }

    private void TryPlayInteractionSound(IInteractable interactable1, IInteractable interactable2, float damage)
    {
        AudioClip interactionClip = SoundInteractableFactory.GetAudioClipByInteraction(interactable1.MaterialType, interactable2.MaterialType);
        if (interactionClip != null)
        {
            float clampedDamageVolume = Mathf.Clamp(damage / 100, 0f, 1f);
            SoundPlayer.PlaySound(interactionClip, clampedDamageVolume);
        }
    }
}