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
    public Vector3 explosiveForceParameters;
    public float lifespan = 1.0f;


    public void Start()
    {
        // Get this rb and collider
        parentCollider = GetComponent<Collider>();
        parentRigidbody = GetComponent<Rigidbody>();

        // Grab all the children parts and store them
        childColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        childRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        childColliders.Remove(parentCollider);
        childRigidbodies.Remove(parentRigidbody);

        // Turn children off
        foreach (Collider childCollider in childColliders) {
            childCollider.enabled = false;
        }
        foreach (Rigidbody childRb in childRigidbodies) {
            childRb.isKinematic = true;
        }


    }

    public void OnCollisionEnter(Collision collision) { 

        // If the collider is an elephant
        ElephantPawn pawn = collision.gameObject.GetComponent<ElephantPawn>();
        if (pawn != null) {

            // Get the collision direction
            Vector3 collisionDirection = collision.relativeVelocity;

            // turn off parent rigidbody and collider
            parentRigidbody.isKinematic = true;
            parentCollider.enabled = false;

            // for each child
            foreach (Collider childCollider in childColliders) {
                // Queue it's demise
                Destroy(childCollider.gameObject, lifespan);

                childCollider.enabled = true;
            }
            // turn on rigidbodies and colliders
            foreach (Rigidbody childRb in childRigidbodies) {
                childRb.isKinematic = false;
                childRb.velocity = collisionDirection;
                // add force to child
                childRb.AddForce(Random.Range(-explosiveForceParameters.x, explosiveForceParameters.x),
                                  Random.Range(0, explosiveForceParameters.y),
                                  Random.Range(-explosiveForceParameters.z, explosiveForceParameters.z));

            }

        }


    }

}
