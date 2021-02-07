using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingDirt : MonoBehaviour
{

	public GameObject myCollision;
	public ParticleSystem dirtParticle;
	public float timeBeforeReset = 8f;
	public float timeToCollapse = 1.6f;

	public Material mat1;
	public Material mat2;
	public Material mat3;

	private float resetTimer;
	private bool particleBurst;
	private bool gone;
	private Renderer thisRenderer;
	private Material currentMat;



    // Start is called before the first frame update
    void Start()
    {
		thisRenderer = GetComponent<Renderer> ();
		currentMat = thisRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
		if (gone == true) {
			//Start the timer.
			resetTimer += Time.deltaTime;
			//Firstly, make it look collapsy here.

			if (resetTimer >= timeToCollapse) {
				thisRenderer.enabled = false;
				if (particleBurst == false) {
					particleBurst = true;
					//Emit particles at the end of collapse.
					dirtParticle.Emit (500);
				}		
			} else if (resetTimer >= timeToCollapse / 4 * 3) {
				thisRenderer.material = mat3;
			} else if (resetTimer >= timeToCollapse / 4 * 2) {
				thisRenderer.material = mat2;
			} else if (resetTimer >= timeToCollapse / 4) {
				thisRenderer.material = mat1;
			}	 		


			//This logic handles the collision

			if (resetTimer >= timeBeforeReset) {
				//Coming back
				resetTimer = 0;
				myCollision.SetActive (true);
				gone = false;
				particleBurst = false;
				thisRenderer.material = currentMat;
				thisRenderer.enabled = true;
			} else if (resetTimer >= timeToCollapse) {
				//Collapse.
				myCollision.SetActive (false);
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (gone == false) {
			Pawn pawn = other.GetComponent<MousePawn> ();
			// If we are a mouse
			if (pawn != null) {
				gone = true;
				if(GetComponent<AudioSource>().isPlaying==false)
				{
					GetComponent<AudioSource> ().Play ();
				}
			}
		}
	}
}
