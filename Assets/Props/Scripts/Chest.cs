using UnityEngine;

public class Chest : DamageableEntity, ILootable, IInteractable
{
    private bool isOpen;

    public void DropLoot(DamageableEntity entity, int LootLevel)
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
