using UnityEngine;
using System.Collections;

public class MouseCharacter : MonoBehaviour
{
    Animator mouseAnimator;
    public bool jumpStart = false;
    public float groundCheckDistance = 0.1f;
    public float groundCheckOffset = 0.01f;
    public bool isGrounded = true;
    public float jumpSpeed = 2f;
    Rigidbody mouseRigid;
    public float forwardSpeed;
    public float turnSpeed;
    public float walkMode = 1f;
    public float jumpStartTime = 0f;
    public float maxWalkSpeed = 1f;

    void Start()
    {
        mouseAnimator = GetComponent<Animator>();
        mouseRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGroundStatus();
        Move();
        jumpStartTime += Time.deltaTime;
        maxWalkSpeed = Mathf.Lerp(maxWalkSpeed, walkMode, Time.deltaTime);
    }

    public void Attack()
    {
        mouseAnimator.SetTrigger("Attack");
    }

    public void Hit()
    {
        mouseAnimator.SetTrigger("Hit");
    }



    public void Sleep()
    {
        mouseAnimator.SetBool("IsSleeping", true);
    }

    public void WakeUp()
    {
        mouseAnimator.SetBool("IsSleeping", false);
    }

    public void EatStart()
    {
        mouseAnimator.SetBool("IsEating", true);
    }
    public void EatEnd()
    {
        mouseAnimator.SetBool("IsEating", false);
    }



    public void Gallop()
    {
        walkMode = 1f;
    }



    public void Walk()
    {
        walkMode = .5f;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            mouseAnimator.SetTrigger("Jump");
            jumpStart = true;
            jumpStartTime = 0f;
            isGrounded = false;
            mouseAnimator.SetBool("IsGrounded", false);
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

        if (jumpStart)
        {
            if (jumpStartTime > .25f)
            {
                jumpStart = false;
                mouseRigid.AddForce((transform.up + transform.forward * forwardSpeed) * jumpSpeed, ForceMode.Impulse);
                mouseAnimator.applyRootMotion = false;
                mouseAnimator.SetBool("IsGrounded", false);
            }
        }

        if (isGrounded && !jumpStart && jumpStartTime > .5f)
        {
            mouseAnimator.applyRootMotion = true;
            mouseAnimator.SetBool("IsGrounded", true);
        }
        else
        {
            if (!jumpStart)
            {
                mouseAnimator.applyRootMotion = false;
                mouseAnimator.SetBool("IsGrounded", false);
            }
        }
    }

    public void Move()
    {
        mouseAnimator.SetFloat("Forward", forwardSpeed);
        mouseAnimator.SetFloat("Turn", turnSpeed);
    }
}
