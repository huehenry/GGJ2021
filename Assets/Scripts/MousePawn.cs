using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePawn : Pawn
{
    public Animator anim;

    public override void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("IsGrounded", true);
        base.Start();
    }

    public override void Jump()
    {
        //anim.SetTrigger("Jump");
        base.Jump();
    }

    public override void MoveForward(float speed)
    {
        anim.SetFloat("Forward", speed);
    }

    public override void Rotate(float speed)
    {
        anim.SetFloat("Turn", speed);
    }



}
