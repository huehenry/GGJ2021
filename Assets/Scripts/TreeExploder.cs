using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExploder : MonoBehaviour
{

    public List<GameObject> treeParts;
    public List<Rigidbody> childRigidbodies;
    public List<Collider> childColliders;
    public Rigidbody parentRigidbody;
    public Vector3 explosiveForceParameters;
    public float minForce;
    public float collisionModifier = 5;
    public float lifespan = 1.0f;
    public GameObject explodingTreeParticleAndSoundObject;


    public void Start()
    {
        // Get this rb and collider
        parentRigidbody = GetComponent<Rigidbody>();

        // Grab all the children parts and store them
        childColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        childRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        childRigidbodies.Remove(parentRigidbody);

        // Turn children off
        foreach (Rigidbody childRb in childRigidbodies) {
            childRb.isKinematic = true;
        }


    }

    public void OnCollisionEnter(Collision collision) {

        // If the collider is an elephant
        ElephantPawn pawn = collision.gameObject.GetComponent<ElephantPawn>();
        if (pawn != null) {

            // Get the collision direction
            //Vector3 collisionDirection = collision.relativeVelocity;
            Vector3 collisionDirection = (transform.position - pawn.transform.position).normalized;

            // turn off parent rigidbody and collider
            parentRigidbody.isKinematic = true;

            // for each child
            foreach (Collider childCollider in childColliders) {
                // Queue it's demise
                Destroy(childCollider.gameObject, lifespan);

                // Split it from the parent
                childCollider.transform.parent = null;

                // Collision off
                childCollider.enabled = false;
            }
            // turn on rigidbodies 
            foreach (Rigidbody childRb in childRigidbodies) {

                childRb.isKinematic = false;
                childRb.AddForce(collisionDirection * collisionModifier, ForceMode.VelocityChange);

                // add force to child
                float xForce = Random.Range(minForce, explosiveForceParameters.x);
                float yForce = Random.Range(minForce, explosiveForceParameters.y);
                float zForce = Random.Range(minForce, explosiveForceParameters.z);

                if (Random.value < 0.5f) { xForce = -xForce; }
                if (Random.value < 0.5f) { zForce = -zForce; }

                childRb.AddRelativeForce(xForce, yForce, zForce, ForceMode.VelocityChange );
                childRb.AddTorque(xForce*100, yForce * 100, zForce * 100);
               

            }

            // Instantiate Jeremy's Amazing Particle Sound Extravaganza
            Instantiate(explodingTreeParticleAndSoundObject, transform.position, transform.rotation);

        }


    }

}
