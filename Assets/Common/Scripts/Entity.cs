using UnityEngine;

[RequireComponent(typeof(Sprite), typeof(Animator))]
public abstract class Entity : MonoBehaviour
{
    public string entityName;

    [HideInInspector]
    public Sprite sprite;

    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Despawn()
    {
        Debug.Log($"{entityName} despawned");
        Destroy(gameObject);
    }
}
