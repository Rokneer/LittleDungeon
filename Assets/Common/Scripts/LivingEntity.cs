using UnityEngine;

public abstract class LivingEntity : DamageableEntity
{
    [Header("Stats")]
    public float stamina = 100;
    public float movementSpeed = 6;
    public float attackPower = 10;
    public float armorPower = 10;

    [HideInInspector]
    public Rigidbody2D rb;

    public bool _isFacingRight = true;
    public bool IsFacingRight
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

    public virtual void OnMove()
    {
        Debug.LogWarning($"WARNING {nameof(OnMove)} method has not been implemented");
    }

    public virtual void OnAttack()
    {
        Debug.LogWarning($"WARNING {nameof(OnMove)} method has not been implemented");
    }

    public void SetFacingDirection(Vector2 moveInput)
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

    public float CalculateAttackPower(Weapon weapon, LivingEntity entity)
    {
        return weapon.multiplier * entity.attackPower;
    }

    public float CalculateDefensePower(Shield shield, LivingEntity entity)
    {
        return shield.multiplier * entity.armorPower;
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
