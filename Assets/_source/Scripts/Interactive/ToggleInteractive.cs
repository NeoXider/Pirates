using UnityEngine;
using UnityEngine.Events;

public class ToggleInteractive : MonoBehaviour
{
    [SerializeField]
    protected bool setBoolOnAwake;

    [SerializeField]
    protected bool isOn = false;

    public UnityEvent<bool> OnChanged;  // Событие, вызываемое при изменении состояния (true - вкл, false - выкл)
    public UnityEvent OnActivated;      // Событие, вызываемое при активации
    public UnityEvent OnDeactivated;    // Событие, вызываемое при деактивации

    private void Start()
    {
        if(setBoolOnAwake)
            Activate(isOn);
    }

    /// <summary>
    /// Переключает состояние объекта (включение/выключение).
    /// </summary>
    public virtual void Toggle()
    {
        isOn = !isOn;
        Activate(isOn);
    }

    /// <summary>
    /// Метод для активации или деактивации объекта.
    /// Переопределите этот метод в наследующих классах.
    /// </summary>
    /// <param name="activate">Флаг активации.</param>
    public virtual void Activate(bool activate)
    {
        // Реализуйте логику для активации/деактивации объекта в дочерних классах
        if (activate)
        {
            OnActivated?.Invoke();
        }
        else
        {
            OnDeactivated?.Invoke();
        }

        OnChanged?.Invoke(isOn);
    }

    void OnValidate()
    {
        if(!Application.isPlaying)
            Activate(isOn);
    }
}
