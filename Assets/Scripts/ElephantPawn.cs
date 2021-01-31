using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantPawn : Pawn
{
	public CameraShake shaker;

	public override void Update()
	{
		if(cameraShake==true)
		{
			shaker.ShakeCamera();
		}
	}

    public override void MoveForward(float speed)
    {
        if (isActive) { 
        anim.SetFloat("Forward", speed);
        base.MoveForward(speed);
        } else {
            anim.SetFloat("Forward", 0);
        }
    }

    public override void Jump()
    {
        if (GameManager.instance.trumpetSound != null) {
            AudioSource.PlayClipAtPoint(GameManager.instance.trumpetSound, transform.position);
            anim.SetTrigger("Trumpet");
        }
    }

}
