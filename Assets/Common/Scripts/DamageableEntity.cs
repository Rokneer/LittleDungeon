using UnityEngine;

public abstract class DamageableEntity : Entity
{
    [HideInInspector]
    public AudioClip deathSFX;
    public StatusEffect currentStatusEffect;

    [Header("Health")]
    public float maxHealth = 80;

    [SerializeField]
    private float _currentHealth = 80;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            if (_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set { _isAlive = value; }
    }

    [Header("Invicibility")]
    public bool isInvicible = false;
    public float invicibilityTime = 0.25f;
    public float timeSinceHit = 0;

    [SerializeField]
    private AudioClip[] damageSFX;

    [SerializeField]
    private AudioClip[] healSFX;

    private void Update()
    {
        if (isInvicible)
        {
            if (timeSinceHit > invicibilityTime)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public virtual void OnHealthChange(float changeAmount, bool isDamage)
    {
        if (IsAlive && !isInvicible)
        {
            if (isDamage)
            {
                Hit(changeAmount);
                return;
            }
            Hit(changeAmount);
        }
    }

    public virtual void Hit(float damage)
    {
        CurrentHealth -= damage;
        isInvicible = true;
        animator.SetTrigger("hit");
        SoundFXManager.Instance.PlayRandomSoundFXClip(damageSFX, transform, 1f);
    }

    private void Heal(float healing)
    {
        CurrentHealth += healing;
        isInvicible = true;
        SoundFXManager.Instance.PlayRandomSoundFXClip(healSFX, transform, 1f);
    }
}
