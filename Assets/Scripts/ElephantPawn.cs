using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantPawn : Pawn
{
	public CameraShake shaker;
	private float startCameraHack = 10f;
	private CameraMover camera;

	public override void Start()
	{
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
		camera = GameManager.instance.elephantCamera.GetComponent<CameraMover>();
		camera.moveSpeed = 0.0005f;
		base.Start();
	}

	public override void FixedUpdate()
	{
		if(cameraShake==true)
		{
			shaker.ShakeCamera();
		}
		startCameraHack -= Time.deltaTime;
		if (startCameraHack <= 0) {
			camera.moveSpeed = Mathf.Lerp (0.0005f, 0.1f, Mathf.Abs (startCameraHack)/10);
			//camera.moveSpeed = 0.1f;
		}
        base.FixedUpdate();
	}

    public override void MoveForward(float speed)
    {
        if (isActive) { 
        	anim.SetFloat("Forward", speed);
        	base.MoveForward(speed);
        } else {
            anim.SetFloat("Forward", 0);
        }

		//Turned off rot clamp for elephant, didn't feel as good to not be able to rampage backwards
		/*
		if (transform.localEulerAngles.y < 90 || transform.localEulerAngles.y > 270) {
			if (transform.localEulerAngles.y < 90) {
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 90, transform.localEulerAngles.z);
			} else {
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 270, transform.localEulerAngles.z);
			}
		}*/
    }

    public override void StartJump()
    {
        if (isActive) {
            if (GameManager.instance.trumpetSound != null) {
                AudioSource.PlayClipAtPoint(GameManager.instance.trumpetSound, transform.position);
                anim.SetTrigger("Trumpet");
            }
        }
    }

}
