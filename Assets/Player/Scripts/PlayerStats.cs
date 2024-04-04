using UnityEngine;

[CreateAssetMenu(menuName = "LittleDungeon/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;

    [Header("Movement")]
    public float movementSpeed;

    [Header("Attack")]
    public float attackPower;

    [Header("Armor")]
    public float armorPower;
    public float armorDurability;

    [Header("Dodge")]
    public float dodgeImpulse;
    public float dodgeTime;
    public float dodgeCooldown;

    [Header("Facing Direction")]
    public bool isFacingRight;
}
