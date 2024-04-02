using UnityEngine;

public class Potion : Equipment
{
    private PotionType potionType;

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
