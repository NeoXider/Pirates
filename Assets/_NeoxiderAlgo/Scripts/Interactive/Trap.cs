using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_Neoxider/" + "Interactive/" + nameof(Trap))]
public class Trap : MonoBehaviour
{
    [SerializeField]
    protected float damage = 10f; // Количество урона, наносимого ловушкой

    public UnityEvent OnDamage;

    /// <summary>
    /// Пытаемся нанести урон объекту, у которого есть компонент HealthSystem.
    /// </summary>
    protected virtual void ApplyDamage(Collider other)
    {
        HealthSystem health = other.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.TakeDamage(damage);
            OnDamage?.Invoke();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        ApplyDamage(collision.collider);
    }
} 