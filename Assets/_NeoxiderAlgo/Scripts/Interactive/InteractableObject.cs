using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_Neoxider/" + "Interactive/" + nameof(InteractableObject))]
public class InteractableObject : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Флаг: можно ли подобрать объект в руки.")]
    [SerializeField] private bool isPickupable = false;
    public bool IsPickupable => isPickupable;

    [Header("Interaction Modes")]
    [Tooltip("Включить взаимодействие по нажатию клавиши.")]
    [SerializeField] private bool interactByKey = true;

    [Tooltip("Флаг: можно ли прикасаться к объекту.")]
    [SerializeField] private bool isContactable = true;

    [Tooltip("Взаимодействовать только с игроком.")]
    [SerializeField] private bool interactWithPlayer = true;

    [Header("Events")]
    [Tooltip("Событие, вызываемое при взаимодействии по нажатию клавиши.")]
    public UnityEvent OnPress;

    [Tooltip("Событие, вызываемое при соприкосновении (триггер или столкновение).")]
    public UnityEvent OnContact;

    private Rigidbody rigidbody;
    private Collider collider;

    protected virtual void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
    }

    /// <summary>
    /// Вызывается извне для инициирования взаимодействия по нажатию (например, при нажатии кнопки).
    /// </summary>
    public virtual void Interact()
    {
        if (interactByKey)
        {
            print("Interact " + gameObject.name);
            OnPress?.Invoke();
        }
    }

    /// <summary>
    /// Вызывается для инициирования взаимодействия при соприкосновении (триггер или столкновение).
    /// </summary>
    public virtual void Contact()
    {
        OnContact?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isContactable)
        {
            if (!interactWithPlayer || interactWithPlayer && IsPlayer(other))
            {
                Contact();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isContactable)
        {
            if (!interactWithPlayer || interactWithPlayer && IsPlayer(collision.collider))
            {
                Contact();
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return other.gameObject.GetComponent<Player>() != null;
    }

    /// <summary>
    /// Вызывается при подборе предмета.
    /// </summary>
    public virtual void DropItem()
    {
        if (rigidbody != null)
        {
            //если предмет физический, то включаем физику (что бы не падал из рук)
            rigidbody.isKinematic = false;

            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    /// <summary>
    /// Вызывается при сбрасывании предмета.
    /// </summary>
    public virtual void PickupItem()
    {
        if (rigidbody != null)
        {
            //если предмет физический, то выключаем физику
            rigidbody.isKinematic = true;

            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}