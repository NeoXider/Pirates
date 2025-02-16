using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float damage = 15f;     // Урон патрона
    [SerializeField] private float lifeTime = 5f;      // Время жизни патрона до автоматического уничтожения

    [Header("Impact Effects")]
    [SerializeField] private GameObject impactEffectPrefab;  // Эффект при попадании (например, частицы)

    private void Start()
    {
        // Уничтожаем патрон через lifeTime секунд, чтобы избежать засорения сцены
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Не наносим урон игроку
        if (Player.Instance != null && collision.gameObject == Player.Instance.gameObject)
            return;

        // Если у объекта есть система здоровья – наносим урон
        HealthSystem health = collision.gameObject.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Если задан эффект при попадании, создаем его в точке столкновения
        if (impactEffectPrefab != null)
        {
            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        }

        // Уничтожаем патрон после столкновения
        Destroy(gameObject);
    }
} 