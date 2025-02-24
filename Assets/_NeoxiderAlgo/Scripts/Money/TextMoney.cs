using System;
using TMPro;
using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Text/" + nameof(TextMoney))]
public class TextMoney : SetText
{
    [SerializeField]
    private bool _levelMoney = false;

    public float amount;
    private Money _money;

    public TextMoney()
    {
        _decimal = 0;
    }

    void Update()
    {
        SetAmount(_levelMoney? Money.Instance.levelMoney : Money.Instance.money);
    }

    private void SetAmount(float count)
    {
        amount = count;
        Set(amount);
    }
}

