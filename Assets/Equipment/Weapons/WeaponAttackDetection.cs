using UnityEngine;

public class WeaponAttackDetection : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            float attackPower = player.CalculateAttackPower(
                player.inventory.rightHandItem.Equipment,
                player
            );
            enemy.OnHealthChange(attackPower, true);
        }
    }
}
