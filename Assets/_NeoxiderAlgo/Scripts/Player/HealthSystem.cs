using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_Neoxider/" + "Player/" + nameof(HealthSystem))]
public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField, Tooltip("Текущее здоровье (инициализация значением maxHealth)")]
    private float currentHealth;

    [Tooltip("Событие, вызываемое при изменении процента здоровья (значение от 0 до 1)")]
    public UnityEvent<float> OnHealthChanged;

    [Tooltip("Событие, вызываемое при получении урона")]
    public UnityEvent<float> OnDamageTaken;

    [Tooltip("Событие, вызываемое при лечении")]
    public UnityEvent<float> OnHealed;

    [Tooltip("Событие, вызываемое при смерти")]
    public UnityEvent OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Наносит урон и обновляет состояние здоровья.
    /// Если здоровье опускается до нуля, вызывается событие OnDeath.
    /// </summary>
    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= amount;
        OnDamageTaken?.Invoke(amount);
        OnHealthChanged?.Invoke(GetHealthPercent());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    /// <summary>
    /// Лечит объект на заданное количество единиц здоровья.
    /// </summary>
    public void Heal(float amount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHealed?.Invoke(amount);
        OnHealthChanged?.Invoke(GetHealthPercent());
    }

    /// <summary>
    /// Возвращает процент оставшегося здоровья (от 0 до 1).
    /// </summary>
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    /// <summary>
    /// Возвращает текущее здоровье.
    /// </summary>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Возвращает максимальное здоровье.
    /// </summary>
    public float GetMaxHealth()
    {
        return maxHealth;
    }
} 