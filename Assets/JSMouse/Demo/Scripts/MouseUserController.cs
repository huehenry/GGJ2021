using UnityEngine;
using System.Collections;

public class MouseUserController : MonoBehaviour
{
    MouseCharacter mouseCharacter;

    void Start()
    {
        mouseCharacter = GetComponent<MouseCharacter>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mouseCharacter.Attack();
        }
        if (Input.GetButtonDown("Jump"))
        {
            mouseCharacter.Jump();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            mouseCharacter.Hit();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            mouseCharacter.EatStart();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            mouseCharacter.EatEnd();
        }



        if (Input.GetKeyDown(KeyCode.N))
        {
            mouseCharacter.Sleep();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            mouseCharacter.WakeUp();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            mouseCharacter.Gallop();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            mouseCharacter.Walk();
        }

        mouseCharacter.forwardSpeed = mouseCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
        mouseCharacter.turnSpeed = Input.GetAxis("Horizontal");
    }

}
