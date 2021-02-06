using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

	public Renderer waterLayerFast;
	public Renderer waterLayerMedium;
	public Renderer waterLayerSlow;

	public float fastSpeed;
	public float medSpeed;
	public float slowSpeed;

	public GameObject waterParticle;


    // Update is called once per frame
    void Update()
    {
		Vector2 speed = waterLayerFast.material.GetTextureOffset ("_MainTex");
		speed.y += fastSpeed * Time.deltaTime;
		waterLayerFast.material.SetTextureOffset ("_MainTex", speed);
		speed = waterLayerMedium.material.GetTextureOffset ("_MainTex");
		speed.y += medSpeed * Time.deltaTime;
		waterLayerMedium.material.SetTextureOffset ("_MainTex", speed);
		speed = waterLayerSlow.material.GetTextureOffset ("_MainTex");
		speed.y += slowSpeed * Time.deltaTime;
		waterLayerSlow.material.SetTextureOffset ("_MainTex", speed);
    }

	public void OnTriggerEnter(Collider other)
	{
		Pawn pawn = other.GetComponent<MousePawn> ();
		// If we are a mouse
		if (pawn != null) {
			//If player immediately jumps in water, prevent bug.
			StopAllCoroutines ();
			StartCoroutine(WaterTeleport(pawn));
			Debug.Log ("SPLASH");
		}
	}

	private IEnumerator WaterTeleport(Pawn pawn)
	{
		MouseHole exitHole = GameManager.instance.lastMouseCheckpoint;

		// Deactivate the exit hole
		exitHole.isActive = false;

		// Kill the pawn movement
		pawn.isActive = false;

		//TODO: Start particle effect and sound
		Instantiate(waterParticle, pawn.transform.position, pawn.transform.rotation);

		// Set the camera to look at this hole, not the mouse!
		CameraMover currentCameraMover = GameManager.instance.currentCamera.GetComponent<CameraMover>();
		currentCameraMover.isActive = false;

		// Deactivate and Move the mouses
		pawn.transform.position = exitHole.transform.position + (Vector3.up * 0.1f);
		pawn.gameObject.SetActive(false);


		// Wait for particles to be enough that we move
		yield return new WaitForSeconds(GameManager.instance.delayAfterEnter);

		//TODO: Start rustling sound
		AudioSource cameraAudio = GameManager.instance.currentCamera.GetComponent<AudioSource>();
		if (cameraAudio && GameManager.instance.moveHoleSound) {
			cameraAudio.PlayOneShot(GameManager.instance.moveHoleSound);
		}

		// Wait for particles to be enough that we move
		yield return new WaitForSeconds(GameManager.instance.delayAfterEnterBeforeMove);


		// Set target of camera to exit hole, so it flies over there on its own
		currentCameraMover.cameraTarget = exitHole.transform;
		currentCameraMover.isActive = true;

		// Wait some amount of time... whatever
		yield return new WaitForSeconds(GameManager.instance.delayMove);

		//TODO: Start exit particles and sound
		Instantiate(GameManager.instance.exitParticle, exitHole.transform.position, exitHole.transform.rotation);
		if(GameManager.instance.exitHoleSound != null) {
			AudioSource.PlayClipAtPoint(GameManager.instance.exitHoleSound, exitHole.transform.position);
		}


		// Wait for particles to be enough that we move
		yield return new WaitForSeconds(GameManager.instance.delayAfterExit);

		// Start pawn movement back up
		exitHole.isActive = false;
		pawn.gameObject.SetActive(true);
		pawn.isActive = true;

		// Look at the pawn again
		currentCameraMover.cameraTarget = pawn.transform;

		// Wait a few seconds before we reactivate the exit
		yield return new WaitForSeconds(4.0f);

		// Deactivate this hole for a while
		exitHole.isActive = true;

		yield return null;
	}


}
