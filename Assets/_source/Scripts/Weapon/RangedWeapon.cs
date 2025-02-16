using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RangedWeapon : MonoBehaviour, IUsable
{
    [Header("Ammo Settings")]
    [SerializeField] private int magazineSize = 10;   // Размер обоймы
    private int currentAmmo;
    [SerializeField] private bool infiniteAmmo = false; // Безлимитные патроны

    [Header("Fire Settings")]
    [SerializeField] private float fireDelay = 0.5f;    // Задержка перед выстрелом
    [SerializeField] private float reloadTime = 2f;       // Перезарядка после выстрела

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;      // Префаб патрона
    [SerializeField] private Transform projectileSpawnPoint;   // Точка спауна патрона
    [SerializeField] private float projectileForce = 500f;       // Сила, придающая скорости патрону

    [Header("Effects")]
    [SerializeField] private GameObject shootEffectPrefab;       // Эффект выстрела (например, вспышка)

    [Header("Shoot Event")]
    public UnityEvent OnShoot;                                   // Событие, вызываемое при выстреле

    private bool isShooting = false;

    private void Awake()
    {
        currentAmmo = magazineSize;
    }

    /// <summary>
    /// Метод использования оружия дальнего боя.
    /// Выстреливает один патрон при наличии боезапаса (или при бесконечных патронах).
    /// </summary>
    public void Use()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        if (!infiniteAmmo && currentAmmo <= 0)
        {
            Debug.Log("RangedWeapon: Нет патронов!");
            yield break;
        }

        isShooting = true;

        // Задержка перед выстрелом (можно использовать для синхронизации с анимацией)
        yield return new WaitForSeconds(fireDelay);

        // Если не бесконечные патроны, уменьшаем их количество
        if (!infiniteAmmo)
        {
            currentAmmo--;
        }

        // Создаем патрон и добавляем ему физическую силу
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(projectileSpawnPoint.forward * projectileForce);
            }
        }
        else
        {
            Debug.LogWarning("RangedWeapon: Не назначены ProjectilePrefab или ProjectileSpawnPoint.");
        }

        // Если назначен эффект выстрела – создаем его
        if (shootEffectPrefab != null)
        {
            Instantiate(shootEffectPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }

        // Вызываем событие выстрела
        OnShoot?.Invoke();

        // Ждем время перезарядки перед следующим выстрелом
        yield return new WaitForSeconds(reloadTime);
        isShooting = false;
    }
} 