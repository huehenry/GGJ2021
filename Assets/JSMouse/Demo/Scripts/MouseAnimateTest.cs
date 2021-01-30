using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAnimateTest : MonoBehaviour
{

    Animator mouseAnimator;

    // Start is called before the first frame update
    void Start()
    {
        mouseAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseAnimator.applyRootMotion = false;
        mouseAnimator.SetBool("IsGrounded", true);
        mouseAnimator.SetFloat("Forward", 10f);
        mouseAnimator.SetFloat("Turn", -10f);
    }
}
