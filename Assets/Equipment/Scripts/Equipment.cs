using UnityEngine;

[CreateAssetMenu(menuName = "LittleDungeon/Equipment")]
public class Equipment : ScriptableObject
{
    public GameObject prefab;
    public string itemName;
    public EquipmentType type;
    public Sprite sprite;
    public StatusEffect statusEffect;
    public float useSpeed;
    public float multiplier;
    public AudioClip pickUpSFX;
    public AudioClip actionSFX;
}
