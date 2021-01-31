using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Controller
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Controller initialized: "+gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            if (!pawn.isJumping) {
                pawn.StartJump();
            }
        } else if (Input.GetButtonUp("Jump")) {
            if (pawn.isJumping) {
                pawn.EndJump();
            }
        }

        pawn.Rotate(Input.GetAxis("Horizontal"));
        pawn.MoveForward(Input.GetAxis("Vertical"));
    }
}
