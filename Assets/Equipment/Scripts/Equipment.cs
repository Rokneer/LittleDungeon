using UnityEngine;

[CreateAssetMenu(menuName = "LittleDungeon/Equipment")]
public class Equipment : ScriptableObject
{
    public string itemName;

    [Space]
    public EquipmentType type;

    [Space]
    public Sprite sprite;

    [Space]
    public StatusEffect statusEffect;

    [Space]
    public float useDelayMultiplier;

    [Space]
    public float multiplier;

    [Space]
    public float staminaCost;

    [Header("Sound Effects")]
    public AudioClip pickUpSFX;
    public AudioClip actionSFX;
}
