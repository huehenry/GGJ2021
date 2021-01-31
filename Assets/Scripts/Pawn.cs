using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("MoveData")]
    public float moveSpeedMax;
    public float acceleration;
    public float turnSpeedMax;
    public float jumpForce;
    public Vector3 jumpDirection = Vector3.up;
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
    public virtual void Update()
    {

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

            // Move
            transform.position = transform.position + (transform.forward * moveSpeedMax * speed * Time.deltaTime);

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

    public virtual void Jump()
    {
        if (isActive) {
            rb.AddForce(transform.TransformDirection(jumpDirection) * jumpForce);
        }
    }
}
