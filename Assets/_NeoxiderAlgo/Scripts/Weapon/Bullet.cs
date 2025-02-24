using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Weapon/" + nameof(Bullet))]
public class Bullet : Trap
{
    [Header("Bullet Settings")]
    [SerializeField] private bool dealDamage = true;         // Если false, пуля не наносит урон при столкновении.
    
    [SerializeField] private bool destroyOnDamage = true;      // Если true, пуля уничтожается после нанесения урона.
    
    [SerializeField] private float lifeTime = 5f;              // Время жизни пули до автоматического уничтожения.
    [SerializeField] private Vector3 initialForce = Vector3.zero; // Сила, которая применяется к пуле при спавне.

    [Header("Impact Effects")]
    [SerializeField] private GameObject impactEffectPrefab; // Эффект при столкновении (например, частицы)

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Применяем силу сразу при спавне (пуля сама к себе применяет силу)
        if (rb != null && initialForce != Vector3.zero)
        {
            rb.AddForce(initialForce, ForceMode.Impulse);
        }
        
        // Уничтожаем пулю спустя время, чтобы не засорять сцену
        Destroy(gameObject, lifeTime);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (dealDamage)
        {
            ApplyDamage(collision.collider);
        }
        if (destroyOnDamage)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (dealDamage)
        {
            ApplyDamage(other);
        }
        if (destroyOnDamage)
        {
            Destroy(gameObject);
        }
    }
} 