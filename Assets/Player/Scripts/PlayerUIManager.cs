using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    private static PlayerUIManager _instance;
    public static PlayerUIManager Instance => _instance;

    private Player player;

    [Header("Health")]
    [SerializeField]
    private Image healthImage;

    [Header("Armor")]
    [SerializeField]
    private Image currentArmorImage;

    [SerializeField]
    private Sprite[] armorSprites;

    [Header("Stamina")]
    [SerializeField]
    private Slider staminaSlider;

    [Header("Potions")]
    [SerializeField]
    private Image[] healthPotions;

    [SerializeField]
    private Image[] staminaPotions;

    [SerializeField]
    private Image[] armorPotions;

    [SerializeField]
    private Image[] curePotions;

    [Header("Weapons")]
    [SerializeField]
    private Image prevWeapon;

    [SerializeField]
    private Image currentWeapon;

    [SerializeField]
    private Image nextWeapon;

    [Header("Shields")]
    [SerializeField]
    private Image prevShield;

    [SerializeField]
    private Image currentShield;

    [SerializeField]
    private Image nextShield;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        player = GetComponent<Player>();
    }

    private void Start()
    {
        staminaSlider.maxValue = player.MaxStamina;
        staminaSlider.value = player.CurrentStamina;
    }

    public void UpdateArmorUI(float currentHealth, float maxHealth)
    {
        float percent80 = maxHealth * 0.8f;
        float percent60 = maxHealth * 0.5f;
        float percent20 = maxHealth * 0.2f;

        if (percent80 <= currentHealth)
        {
            currentArmorImage.sprite = armorSprites[2];
        }
        else if (percent60 <= currentHealth)
        {
            currentArmorImage.sprite = armorSprites[1];
        }
        else if (percent20 <= currentHealth)
        {
            currentArmorImage.sprite = armorSprites[0];
        }
    }

    public void UpdateStaminaUI(float value)
    {
        staminaSlider.value = value;
    }

    public void UpdatePotionUI(PotionType potionType, int potionAmount)
    {
        switch (potionType)
        {
            case PotionType.Health:
                ChangePotionSprite(healthPotions, potionAmount);
                break;
            case PotionType.Stamina:
                ChangePotionSprite(staminaPotions, potionAmount);
                break;
            case PotionType.Cure:
                ChangePotionSprite(curePotions, potionAmount);
                break;
            case PotionType.Armor:
                ChangePotionSprite(armorPotions, potionAmount);
                break;
        }
    }

    private void ChangePotionSprite(Image[] potionImages, int potionAmount)
    {
        switch (potionAmount)
        {
            case 0:
                potionImages[0].color = Color.black;
                potionImages[1].color = Color.black;
                potionImages[2].color = Color.black;
                break;
            case 1:
                potionImages[0].color = Color.black;
                potionImages[1].color = Color.black;
                potionImages[2].color = Color.white;
                break;
            case 2:
                potionImages[0].color = Color.black;
                potionImages[1].color = Color.white;
                potionImages[2].color = Color.white;
                break;
            case 3:
                potionImages[0].color = Color.white;
                potionImages[1].color = Color.white;
                potionImages[2].color = Color.white;
                break;
        }
    }
}
