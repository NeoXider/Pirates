using UnityEngine;

public class Flashlight : MonoBehaviour, IUsable
{
    [SerializeField]
    private GameObject lightObject; // Объект, отвечающий за освещение (например, компонент Light)

    private bool isOn = false;

    private void Start()
    {
        if (lightObject != null)
        {
            lightObject.SetActive(isOn);
        }
    }

    /// <summary>
    /// Переключает состояние фонарика (включение/выключение).
    /// </summary>
    public void Use()
    {
        isOn = !isOn;
        if (lightObject != null)
        {
            lightObject.SetActive(isOn);
        }
        else
        {
            // Если отдельно не назначен объект для света, переключаем активность самого объекта
            gameObject.SetActive(isOn);
        }
    }
} 