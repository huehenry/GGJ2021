using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExploder : MonoBehaviour
{

    public List<GameObject> treeParts;
    public List<Rigidbody> childRigidbodies;
    public List<Collider> childColliders;
    public Rigidbody parentRigidbody;
    public Collider parentCollider;


    public void Start()
    {
        // Get this rb and collider

        // Grab all the children parts and store them

    }

    public void OnCollisionEnter(Collision collision)
    {
        // If the collider is an elephant

            // Get the collision direction

            // turn off parent rigidbody and collider
    
            // for each child
                // turn on rigidbodies and colliders
                // add force to child




    }

}
