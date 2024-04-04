using UnityEngine;

public abstract class LivingEntity : DamageableEntity
{
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Stamina")]
    [SerializeField]
    private float _maxStamina = 100;
    public virtual float MaxStamina
    {
        get => _maxStamina;
        set => _maxStamina = value;
    }

    [SerializeField]
    private float _currentStamina = 100;
    public virtual float CurrentStamina
    {
        get => _currentStamina;
        set => _currentStamina = value;
    }

    [Header("Movement")]
    [SerializeField]
    private float _movementSpeed = 6;
    public virtual float MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }

    [Header("Attack")]
    [SerializeField]
    private float _attackPower = 10;
    public virtual float AttackPower
    {
        get => _attackPower;
        set => _attackPower = value;
    }

    [Header("Armor")]
    [SerializeField]
    private float _armorPower = 10;
    public virtual float ArmorPower
    {
        get => _armorPower;
        set => _armorPower = value;
    }

    [SerializeField]
    private float _armorDurability = 10;
    public virtual float ArmorDurability
    {
        get => _armorDurability;
        set => _armorDurability = value;
    }

    [Header("Facing Direction")]
    [SerializeField]
    private bool _isFacingRight = true;
    public virtual bool IsFacingRight
    {
        get => _isFacingRight;
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void OnMove()
    {
        Debug.LogWarning($"WARNING {nameof(OnMove)} method has not been implemented");
    }

    public virtual void OnAttack()
    {
        Debug.LogWarning($"WARNING {nameof(OnAttack)} method has not been implemented");
    }

    //! Consider replacing with pointer data instead of movement data in player
    public virtual void SetFacingDirection(Vector2 moveInput)
    {
        switch (moveInput.x)
        {
            case > 0 when !IsFacingRight:
                IsFacingRight = true;
                break;
            case < 0 when IsFacingRight:
                IsFacingRight = false;
                break;
            case 0:
                return;
        }
    }

    public float CalculateAttackPower(Equipment weapon, LivingEntity entity)
    {
        return weapon.multiplier * entity.AttackPower;
    }

    public float CalculateDefensePower(Equipment shield, LivingEntity entity)
    {
        return shield.multiplier * entity.ArmorPower;
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
        CauseKnockback(Vector2.zero);
    }

    private void CauseKnockback(Vector2 knockbackPower)
    {
        Vector2 deliveredKnockback = Vector2.zero;
        if (knockbackPower != Vector2.zero)
        {
            deliveredKnockback =
                transform.parent.localScale.x > 0
                    ? knockbackPower
                    : new Vector2(-knockbackPower.x, knockbackPower.y);
        }
        rb.velocity = new Vector2(knockbackPower.x, rb.velocity.y + knockbackPower.y);
    }
}
