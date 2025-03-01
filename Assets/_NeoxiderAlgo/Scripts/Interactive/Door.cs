using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Interactive/" + nameof(Door))]
public class Door : InteractableObject
{
    public Animator animator;
    public string boolName = "Open";
    public bool isOn;

    void Start()
    {
        OnPress.AddListener(ToggleDoor);
        animator.SetBool(boolName, isOn);
    }

    private void OnDestroy()
    {
        OnPress.RemoveListener(ToggleDoor);
    }

    public void ToggleDoor()
    {
        isOn = !isOn;
        animator.SetBool(boolName, isOn);
    }

    void OnValidate()
    {
        animator ??=GetComponent<Animator>();
    }
}
