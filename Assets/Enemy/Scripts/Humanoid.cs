using UnityEngine;

public class Humanoid : Enemy
{
    [SerializeField]
    private Equipment rightHandEquipment;

    [SerializeField]
    private Equipment leftHandEquipment;

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }


}
