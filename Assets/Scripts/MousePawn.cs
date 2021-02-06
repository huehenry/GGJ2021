using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePawn : Pawn
{
    public override void Start()
    {
        anim.applyRootMotion = false;
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void Die()
    {

    }

    public override void StartJump()
    {
        if (isGrounded || coyoteTime<=0.15f) {
            if (GameManager.instance.mouseJump != null) {
                AudioSource.PlayClipAtPoint(GameManager.instance.mouseJump, transform.position);
            }
            anim.SetTrigger("Jump");
            base.StartJump();
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
			//Audio volume
			if (isGrounded == true) {
				MainMenuController._mainMenu.audio.mouseWalking = speed;
			} else {
				MainMenuController._mainMenu.audio.mouseWalking = 0;
			}
        }
        else {
            anim.SetFloat("Forward", 0);
			MainMenuController._mainMenu.audio.mouseWalking = 0;
        }
    }

    public override void Rotate(float speed)
    {
        if (isActive) {
            anim.SetFloat("Turn", speed);
            base.Rotate(speed);
        }
    }



}
