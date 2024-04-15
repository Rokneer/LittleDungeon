using UnityEngine;

public abstract class Enemy : LivingEntity, ILootable
{
    public override float CurrentHealth
    {
        set
        {
            base.CurrentHealth = value;
            if (!IsAlive)
            {
                DropLoot();
            }
        }
    }

    [SerializeField]
    private float _lootLevel = 1;
    public float LootLevel
    {
        get => _lootLevel;
        set => _lootLevel = value;
    }

    [SerializeField]
    private float attackPower = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.OnHealthChange(attackPower, true);
        }
    }

    public void DropLoot()
    {
        Debug.Log($"Dropped loot of level {LootLevel}");
    }
}
