using UnityEngine;

public class Potion : Equipment
{
    private PotionType potionType;

    private void Awake()
    {
        equipmentType = EquipmentType.Potion;
    }

    public override void OnDespawn()
    {
        Debug.Log("Potion of type" + potionType + "despawned");
    }

    public override void OnPickUp()
    {
        Debug.Log("Potion of type" + potionType + "was picked up");
    }

    public override void OnUse()
    {
        Debug.Log("Potion of type" + potionType + "was used");
    }
}
