using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("MoveData")]
    public Vector3 moveVector = Vector3.zero;
    public float moveSpeedMax;
    public float jumpMoveSpeedMax;
    public float turnSpeedMax;
    public float jumpStart;
    public float jumpAcceleration;
    public float jumpStartY;
    public Vector3 jumpDirection = Vector3.up;
    public Vector3 maxJumpVelocity;
    public float maxJumpHeight = 1;
    public Vector3 jumpVelocity;
    public bool isJumping = false;
    public bool isGrounded = false;
    [Header("HealthData")]
    public int lives = 3;
    [Header("Components")]
    public Animator anim;
    public Rigidbody rb;
    [Header("Other")]
    public bool isActive;
    public ParticleSystem trail;
    [Header("Map Limits")]
    public float xMin;
    public float zMin;
    public float xMax;
    public float zMax;
	public bool cameraShake;

	private float timer;
	private float startJumpTime;
	public float coyoteTime= 0.0f;


    // Start is called before the first frame update
    public virtual void Start()
    {
        if (rb == null) {
            rb = GetComponent<Rigidbody>();
        }

        if (anim == null) {
            anim = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
		timer += Time.deltaTime;

        // Check for grounded
        Ray theRay = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        if (Physics.Raycast(theRay, out hitInfo, 0.1f)) {
            isGrounded = true;
			coyoteTime = 0;
        }
        else {
            isGrounded = false;
			coyoteTime += Time.deltaTime;
        }

        // Process jump
        if (isJumping) {
            StayJump();
        }

        // Move our move vector
        rb.MovePosition(rb.position + moveVector);
    }

    public virtual void MoveForward(float speed)
    {
        // If we are moving, move
        if (isActive) {

            // Particle Trail
            if (speed > 0.1f) {
				cameraShake = true;
                if (trail != null) {
                    trail.Play();
                }
            }
            else {
				cameraShake = false;
                if (trail != null) {
                    trail.Stop();
                }
            }
				
            if (!isGrounded) {
                moveVector = transform.forward * jumpMoveSpeedMax * speed;
            }
            else {
                moveVector = transform.forward * moveSpeedMax * speed; 
            }

            // Limits
            if (transform.position.x > xMax) {
                transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
            }
            if (transform.position.z > zMax) {
                transform.position = new Vector3(transform.position.x, transform.position.y, zMax);
            }
            if (transform.position.x < xMin) {
                transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
            }
            if (transform.position.z < zMin) {
                transform.position = new Vector3(transform.position.x, transform.position.y, zMin);
            }
        }
    }

    public virtual void Rotate(float speed)
    {
        if (isActive) {
            transform.Rotate(0, speed * turnSpeedMax * Time.deltaTime, 0);
        }
    }

    public virtual void StartJump()
    {
        if ( isActive ) {
            // Start jump
            isJumping = true;

            // TEST: No gravity while jumping?
            rb.useGravity = false;

            // Set our starting jump velocity
            jumpVelocity = jumpDirection * jumpStart;

            // Save our start y
            jumpStartY = transform.position.y;

			startJumpTime = timer;

			//Don't allow a double jump
			coyoteTime = 0.25f;
        }
    }

    public virtual void EndJump()
    {
        rb.useGravity = true;
        isJumping = false;

    }


    public virtual void StayJump()
    {
        //Debug.Log("JUMPING");

        // Increase our velocity by jump values, 
        jumpVelocity += jumpDirection * (jumpAcceleration * Time.deltaTime);

        // but don't go over the max jump velocity
        jumpVelocity.y = Mathf.Clamp(jumpVelocity.y, 0, maxJumpVelocity.y);

        // Move based on that velocity
        moveVector += transform.TransformDirection(jumpVelocity);

        //
        /*if (transform.position.y >= jumpStartY + maxJumpHeight) {
            //rb.MovePosition(new Vector3(transform.position.x, jumpStartY + maxJumpHeight, transform.position.z));
			//Debug.Log(transform.position.y-jumpStartY + maxJumpHeight);
            EndJump();
        }*/

		if (timer - startJumpTime > 0.25f) {
			EndJump ();
		}
    }

    public Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        Vector3 temp;
        temp.x = Mathf.Clamp(value.x, min.x, max.x);
        temp.y = Mathf.Clamp(value.y, min.y, max.y);
        temp.z = Mathf.Clamp(value.z, min.z, max.z);
        return temp;
    }

}
