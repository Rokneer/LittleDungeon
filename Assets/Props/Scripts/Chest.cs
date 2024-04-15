using UnityEngine;

public class Chest : DamageableEntity, ILootable, IInteractable
{
    private bool isOpen;

    public void DropLoot()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
