using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f; // Количество урона, наносимого ловушкой

    public UnityEvent OnDamage;

    /// <summary>
    /// Пытаемся нанести урон объекту, у которого есть компонент HealthSystem.
    /// </summary>
    private void ApplyDamage(Collider other)
    {
        HealthSystem health = other.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.TakeDamage(damage);
            OnDamage?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ApplyDamage(collision.collider);
    }
} 