using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Interactive/" + nameof(Flashlight))]

public class Flashlight : ToggleInteractive, IUsable
{
    [SerializeField]
    private GameObject[] lightObjects = new GameObject[0]; // Объекты, отвечающие за освещение (например, компонент Light)

    public override void Activate(bool activate)
    {
        base.Activate(activate);

        for (int i = 0; i < lightObjects.Length; i++)
        {
            lightObjects[i].SetActive(activate);
        }
    }

    public void Use()
    {
        Toggle();
    }
}