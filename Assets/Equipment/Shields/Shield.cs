using UnityEngine;

public class Shield : Equipment
{
    private ShieldType shieldType;

    private void Awake() 
    {
        equipmentType = EquipmentType.Shield;
    }

    public override void OnDespawn()
    {
        Debug.Log("Shield of type" + shieldType + "despawned");
    }

    public override void OnPickUp()
    {
        Debug.Log("Shield of type" + shieldType + "was picked up");
    }

    public override void OnUse()
    {
        Debug.Log("Shield of type" + shieldType + "was used");
    }
}
