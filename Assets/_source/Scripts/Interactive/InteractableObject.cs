using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Флаг: можно ли подобрать объект в руки.")]
    [SerializeField] private bool isPickupable = false;
    public bool IsPickupable => isPickupable;

    [Header("Interaction Modes")]
    [Tooltip("Включить взаимодействие по нажатию клавиши.")]
    [SerializeField] private bool interactByKey = true;

    [Tooltip("Взаимодействовать только с игроком.")]
    [SerializeField] private bool interactWithPlayer = true;

    [Header("Events")]
    [Tooltip("Событие, вызываемое при взаимодействии по нажатию клавиши.")]
    public UnityEvent OnPress;

    [Tooltip("Событие, вызываемое при соприкосновении (триггер или столкновение).")]
    public UnityEvent OnContact;

    private Rigidbody rigidbody;
    private Collider collider;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
    }

    /// <summary>
    /// Вызывается извне для инициирования взаимодействия по нажатию (например, при нажатии кнопки).
    /// </summary>
    public void InteractByKey()
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
    private void Contact()
    {
        OnContact?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!interactWithPlayer || interactWithPlayer && IsPlayer(other))
        {
            Contact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!interactWithPlayer || interactWithPlayer && IsPlayer(collision.collider))
        {
            Contact();
        }
    }

    private bool IsPlayer(Collider other)
    {
        return other.gameObject.GetComponent<Player>() != null;
    }

    /// <summary>
    /// Вызывается при подборе предмета.
    /// </summary>
    public void DropItem()
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
    public void PickupItem()
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