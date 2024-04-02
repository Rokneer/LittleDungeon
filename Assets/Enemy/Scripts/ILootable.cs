using UnityEngine;

public interface ILootable
{
    void DropLoot(DamageableEntity entity, int LootLevel);
}
