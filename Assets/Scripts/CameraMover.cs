using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

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
        // Every frame look at the offset in front of the target
        transform.LookAt(cameraTarget.position + lookOffset, Vector3.up);
        
        // Every frame,move towards the target, but don't rotate
        transform.position = Vector3.MoveTowards(transform.position, cameraTarget.position + targetOffset, moveSpeed * Time.deltaTime);          
    }
}
