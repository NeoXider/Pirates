using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_Neoxider/" + "Weapon/" + nameof(MeleeWeapon))]

public class MeleeWeapon : InteractableObject, IUsable
{
    [Space]
    [Header("Attack Settings")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float attackDelay = 0.3f; // Задержка перед атакой
    [SerializeField] private float cooldown = 2f;      // Перезарядка между атаками
    [SerializeField] private float attackRadius = 1f;    // Радиус эффекта атаки
    [SerializeField] private Vector3 offset;  // Расстояние от текущего трансформа, куда проводится атака

    [Header("Layer Settings")]
    [SerializeField] private LayerMask ignoreLayers;     // Слои, которые будут игнорироваться (остальные проверяются)

    [Header("Attack Event")]
    public UnityEvent OnAttack;                          // Событие, вызываемое после нанесения атаки

    private bool isAttacking = false;

    private void Start()
    {
        OnPress.AddListener(Use);
    }

    private void OnDestroy()
    {
        OnPress.RemoveListener(Use);
    }

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
        Vector3 attackCenter = transform.position + offset;
        
        // Получаем все коллайдеры в области атаки, игнорируя указанные слои (~ - побитовое отрицание)
        Collider[] hitColliders = Physics.OverlapSphere(attackCenter, attackRadius, ~ignoreLayers);
        foreach (Collider hit in hitColliders)
        {
            // Если объект является игроком – можно продолжить игнорирование (если игрок не включён в ignoreLayers)
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
        Vector3 attackCenter = transform.position + offset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, attackRadius);
    }
} 