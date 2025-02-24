using System;
using TMPro;
using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Text/" + nameof(SetText))]
public class SetText : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text _text;

    [SerializeField]
    protected int _decimal = 2;

    public string startAdd = "";
    public string endAdd = "";


    public void Set(int value)
    {
        Set(value.ToString());
    }

    public void Set(float value)
    {
        Set((int)Math.Round(value, _decimal));
    }

    public void Set(string value)
    {
        string text = startAdd + value + endAdd;
        _text.text = text;
    }

    private void OnValidate()
    {
        if (_text == null)
        {
            _text = GetComponent<TMP_Text>();
        }
    }
}