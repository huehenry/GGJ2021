using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("MoveData")]
    public float moveSpeedMax;
    public float turnSpeedMax;
    public float jumpForce;
    [Header("HealthData")]
    public int lives = 3;
    [Header("Components")]
    [SerializeField] private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveForward(float speed)
    {
        transform.position += transform.forward * moveSpeedMax * speed * Time.deltaTime;
    }

    public void Rotate (float speed)
    {
        transform.Rotate(0, speed * turnSpeedMax * Time.deltaTime, 0);
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
}
