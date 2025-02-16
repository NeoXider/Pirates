using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : MonoBehaviour, IUsable
{
    [Header("Attack Settings")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float attackDelay = 0.3f; // Задержка перед атакой
    [SerializeField] private float cooldown = 2f;      // Перезарядка между атаками
    [SerializeField] private float attackRadius = 1f;    // Радиус эффекта атаки
    [SerializeField] private float attackDistance = 1f;  // Расстояние от текущего трансформа, куда проводится атака

    [Header("Layer Settings")]
    [SerializeField] private LayerMask attackLayers;     // Слой (или слои) для проверки попадания (например, враги)

    [Header("Attack Event")]
    public UnityEvent OnAttack;                          // Событие, вызываемое после нанесения атаки

    private bool isAttacking = false;

    /// <summary>
    /// Метод использования (удар) оружием ближнего боя.
    /// Если оружие не на перезарядке, запускает процедуру атаки.
    /// </summary>
    public void Use()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        // Ожидание задержки перед атакой (для синхронизации с анимацией и т.п.)
        yield return new WaitForSeconds(attackDelay);

        // Вычисляем центр сферы атаки: вперед от текущего трансформа
        Vector3 attackCenter = transform.position + transform.forward * attackDistance;
        
        // Получаем все коллайдеры в заданной области
        Collider[] hitColliders = Physics.OverlapSphere(attackCenter, attackRadius, attackLayers);
        foreach (Collider hit in hitColliders)
        {
            // Исключаем атаку по игроку
            if (Player.Instance != null && hit.gameObject == Player.Instance.gameObject)
                continue;

            // Если у объекта есть HealthSystem – наносим урон
            HealthSystem health = hit.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        // Вызываем событие атаки
        OnAttack?.Invoke();

        // Ждем время перезарядки перед следующим ударом
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
    }

    // Для отладки в редакторе рисуем сферу атаки
    private void OnDrawGizmosSelected()
    {
        Vector3 attackCenter = transform.position + transform.forward * attackDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, attackRadius);
    }
} 