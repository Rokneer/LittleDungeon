using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Equipment weapon;
    public Vector2 pointerPosition;

    

    private void Update()
    {
        Vector2 direction = (pointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
    }
}
