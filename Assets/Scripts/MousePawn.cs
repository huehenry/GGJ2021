using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePawn : Pawn
{
    public bool isGrounded;

    public override void Start()
    {
        anim.applyRootMotion = false;
        base.Start();
    }

    public override void Update()
    {
        Ray theRay = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        if (Physics.Raycast(theRay, out hitInfo, 0.1f)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
        anim.SetBool("IsGrounded", isGrounded);

    }

    public void Die()
    {

    }

    public override void Jump()
    {
        if (isGrounded) {
            if (GameManager.instance.mouseJump != null) {
                AudioSource.PlayClipAtPoint(GameManager.instance.mouseJump, transform.position);
            }

            anim.SetTrigger("Jump");
            base.Jump();
        }
    }

    public override void MoveForward(float speed)
    {
        // If going backwards, slow down
        if (speed <0) {
            speed *= 0.5f;
        }
        if (isActive) {
            anim.SetFloat("Forward", speed);
            base.MoveForward(speed);
        }
        else {
            anim.SetFloat("Forward", 0);
        }
    }

    public override void Rotate(float speed)
    {
        anim.SetFloat("Turn", speed);
        base.Rotate(speed);
    }



}
