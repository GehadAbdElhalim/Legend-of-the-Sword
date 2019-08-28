using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Cancel()
    {
        Movement.canGetInput = false;
        animator.SetInteger("Combo", Movement.comboCounter);
    }
}
