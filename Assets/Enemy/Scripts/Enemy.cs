using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Enemy : LivingEntity, ILootable
{
    [HideInInspector]
    public CircleCollider2D aggroTrigger;
    public float _aggroRange;
    public float AggroRange
    {
        get => _aggroRange;
        set
        {
            _aggroRange = value;
            aggroTrigger.radius = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        aggroTrigger = GetComponent<CircleCollider2D>();
    }

    public override void OnMove()
    {
        throw new System.NotImplementedException();
    }

    public override void OnAttack()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ChangeState()
    {
        throw new System.NotImplementedException();
    }

    public void DropLoot(DamageableEntity entity, int LootLevel)
    {
        throw new System.NotImplementedException();
    }
}
