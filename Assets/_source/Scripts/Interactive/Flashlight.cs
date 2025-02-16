using UnityEngine;

public class Flashlight : MonoBehaviour, IUsable
{
    [SerializeField]
    private GameObject[] lightObjects; // Объекты, отвечающий за освещение (например, компонент Light)

    public bool isOn = false;

    private void Start()
    {
        Activate(isOn);
    }

    private void Activate(bool activate)
    {
        for (int i = 0; i < lightObjects.Length; i++)
        {
            lightObjects[i].SetActive(activate);
        }
    }

    /// <summary>
    /// Переключает состояние фонарика (включение/выключение).
    /// </summary>
    public void Use()
    {
        isOn = !isOn;
        Activate(isOn);
    }

    void OnValidate()
    {
        Activate(isOn);
    }
} 