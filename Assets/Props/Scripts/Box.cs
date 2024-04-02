using UnityEngine;

public class Box : DamageableEntity, ILootable
{
    public void DropLoot(DamageableEntity entity, int LootLevel)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }

}
