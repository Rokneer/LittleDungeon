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

    public abstract void OnDespawn();
}
