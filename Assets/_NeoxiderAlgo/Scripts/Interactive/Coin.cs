using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_Neoxider/" + "Interactive/" + nameof(Coin))]
public class Coin : MonoBehaviour
{
    [SerializeField] private InteractableObject interactableObject;
    [SerializeField] private bool destroyOnTake = true;
    public int amount = 1;

    [Tooltip("Монета на уровень")]
    public bool levelMoney = true;
    public UnityEvent<int> OnTake;

    void OnEnable()
    {
        interactableObject.OnContact.AddListener(Take);
    }

    void OnDisable()
    {
        interactableObject.OnContact.RemoveListener(Take);
    }

    public void Take()
    {
        if (levelMoney)
        {
            Money.Instance.AddLevelMoney(amount);
        }
        else
        {
            Money.Instance.Add(amount);
        }
        
        OnTake?.Invoke(amount);

        if (destroyOnTake)
        {
            Destroy(gameObject);
        }
    }

    void OnValidate()
    {
        interactableObject ??= GetComponent<InteractableObject>();
    }
}
