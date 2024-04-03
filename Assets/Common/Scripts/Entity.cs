using UnityEngine;

[RequireComponent(typeof(Sprite), typeof(Animator))]
public abstract class Entity : MonoBehaviour
{
    public string entityName;

    [HideInInspector]
    private Sprite _sprite;
    public Sprite Sprite{
        get => _sprite;
        set {
            _sprite = value;
            spriteRenderer.sprite = value;
        }
    }

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Despawn()
    {
        Debug.Log($"{entityName} despawned");
        Destroy(gameObject);
    }
}
