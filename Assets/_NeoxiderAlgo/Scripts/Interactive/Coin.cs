using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("_Neoxider/" + "Interactive/" + nameof(Coin))]
public class Coin : InteractableObject
{
    [SerializeField] private bool destroyOnTake = true;
    public int amount = 1;

    [Tooltip("Монета на уровень")]
    public bool levelMoney = true;
    public UnityEvent<int> OnTake;

    void OnEnable()
    {
        OnContact.AddListener(Take);
        OnPress.AddListener(Take);
    }

    void OnDisable()
    {
        OnContact.RemoveListener(Take);
        OnPress.RemoveListener(Take);
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

    }
}
