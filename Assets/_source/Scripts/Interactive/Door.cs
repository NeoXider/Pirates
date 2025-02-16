using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";
    public bool isOn;

    void Start()
    {
        animator.SetBool(boolName, isOn);
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
