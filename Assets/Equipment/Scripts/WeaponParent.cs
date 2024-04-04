using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 pointerPosition;

    private void Update()
    {
        Vector2 direction = (pointerPosition - (Vector2)transform.position).normalized;

        transform.right = direction;

        //! TODO: Consider managing rotating based on mouse position
        /* Vector2 scale = transform.localScale;
        switch (direction.x)
        {
            case < 0:
                scale.y = -1;
                break;
            case > 0:
                scale.y = 1;
                break;
            default:
                break;
        }
        transform.localScale = scale; */
    }
}
