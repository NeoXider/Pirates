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

    [Tooltip("Включить автоматическое взаимодействие при входе в триггер.")]
    [SerializeField] private bool interactOnTrigger = false;

    [Tooltip("Включить автоматическое взаимодействие при столкновении.")]
    [SerializeField] private bool interactOnCollision = false;

    [Header("Events")]
    [Tooltip("Событие, вызываемое при взаимодействии по нажатию клавиши.")]
    public UnityEvent OnPress;
    
    [Tooltip("Событие, вызываемое при соприкосновении (триггер или столкновение).")]
    public UnityEvent OnContact;

    /// <summary>
    /// Вызывается извне для инициирования взаимодействия по нажатию (например, при нажатии кнопки).
    /// </summary>
    public void InteractByKey()
    {
        if (interactByKey)
        {
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
        if (interactOnTrigger)
        {
            Contact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interactOnCollision)
        {
            Contact();
        }
    }
} 