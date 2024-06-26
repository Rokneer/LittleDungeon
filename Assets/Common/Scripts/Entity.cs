using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected string entityName;

    [HideInInspector]
    private Sprite _sprite;
    public Sprite Sprite
    {
        get => _sprite;
        set
        {
            _sprite = value;
            if (spriteRenderer)
            {
                spriteRenderer.sprite = value;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = value;
            }
        }
    }

    [HideInInspector]
    protected SpriteRenderer spriteRenderer;

    [HideInInspector]
    protected Animator animator;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void Despawn()
    {
        Debug.Log($"{entityName} despawned");
        Destroy(gameObject);
    }
}
