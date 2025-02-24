using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("_Neoxider/" + "Player/"+ nameof(CharacterAnimation))]
public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;

    [Space]
    [SerializeField]
    private string moveXFloat = "x";

    [SerializeField]
    private string moveYFloat = "y";

    [SerializeField] 
    private string jumpTrigger = "jump";

    [SerializeField]
    private string attackTrigger = "attack";

    [SerializeField]
    private string runSpeed = "runSpeed";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(moveXFloat, Input.GetAxis("Horizontal"));
        animator.SetFloat(moveYFloat, Input.GetAxis("Vertical"));
    }

    public void Jump()
    {
        animator.SetTrigger(jumpTrigger);
    }

    public void Attack()
    {
        animator.SetTrigger(attackTrigger);
    }
}
