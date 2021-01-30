using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public bool isActive;
    public Transform cameraTarget;
    public float moveSpeed;
    public Vector3 targetOffset;
    public Vector3 lookOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            // Every frame,move towards the target, but don't rotate
            transform.position = Vector3.Lerp(transform.position, cameraTarget.position + targetOffset, moveSpeed);
        }
    }
}
