using UnityEngine;

public class Door : Entity, IInteractable
{
    private bool isOpen;

    public void OnOpen()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
