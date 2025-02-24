using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("_Neoxider/" + "UI/" + nameof(HealthUIController))]
public class HealthUIController : MonoBehaviour
{
    [Header("UI элементы для отображения здоровья")]
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Image healthFillImage;

    [SerializeField]
    private Text healthText;

    private void Start()
    {

    }

    private void Update()
    {
        UpdateUI();
    }

    /// <summary>
    /// Обновляет все привязанные UI элементы.
    /// </summary>
    private void UpdateUI()
    {
        if (Player.Instance != null)
        {
            if (healthSlider != null)
            {
                healthSlider.value = Player.Instance.healthSystem.GetHealthPercent();
            }
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = Player.Instance.healthSystem.GetHealthPercent();
            }
            if (healthText != null)
            {
                float currentHealth = Player.Instance.healthSystem.GetCurrentHealth();
                float maxHealth = Player.Instance.healthSystem.GetMaxHealth();
                healthText.text = $"{currentHealth:0}/{maxHealth:0}";
            }
        }
    }
}