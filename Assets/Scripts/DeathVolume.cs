using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolume : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        MousePawn pawn = other.GetComponent<MousePawn>();
        if (pawn != null) {
            //StartCoroutine(DoHoleDeath(pawn));
			StartCoroutine(DoHoleDeathAlso(pawn));
        }
    }
    
    public IEnumerator DoHoleDeath(Pawn pawn) { 
        // Deactivate the hole so we don't instant teleport
        GameManager.instance.lastMouseCheckpoint.isActive = false;

        // Kill the pawn movement
        pawn.isActive = false;

        // Death Noise
        if (GameManager.instance.enterHoleSound != null) {
            AudioSource.PlayClipAtPoint(GameManager.instance.enterHoleSound, transform.position);
        }

        // TODO:  Some sort of fade? Or teleport camera?
        CameraMover currentCameraMover = GameManager.instance.currentCamera.GetComponent<CameraMover>();
        currentCameraMover.transform.position = GameManager.instance.lastMouseCheckpoint.transform.position + currentCameraMover.targetOffset;

        // Deactivate and Move the mouses
        pawn.transform.position = GameManager.instance.lastMouseCheckpoint.transform.position + (Vector3.up * 0.1f);
        pawn.gameObject.SetActive(false);

        // Wait for respawn time
        yield return new WaitForSeconds(GameManager.instance.respawnTime);

        //TODO: Start exit particles and sound
        Instantiate(GameManager.instance.exitParticle, GameManager.instance.lastMouseCheckpoint.transform.position, GameManager.instance.lastMouseCheckpoint.transform.rotation);
        if (GameManager.instance.exitHoleSound != null) {
            AudioSource.PlayClipAtPoint(GameManager.instance.exitHoleSound, GameManager.instance.lastMouseCheckpoint.transform.position);
        }


        // Wait for particles to be enough that we move
        yield return new WaitForSeconds(GameManager.instance.delayAfterExit);

        // Start pawn movement back up
        pawn.gameObject.SetActive(true);
        pawn.isActive = true;

        // Look at the pawn again
        currentCameraMover.cameraTarget = pawn.transform;

        // Wait a few seconds before we reactivate the exit
        yield return new WaitForSeconds(3.0f);

        // Deactivate this hole for a while
        GameManager.instance.lastMouseCheckpoint.isActive = true;

        yield return null;
    }

	private IEnumerator DoHoleDeathAlso(Pawn pawn)
	{

		MouseHole exitHole = GameManager.instance.lastMouseCheckpoint;

		// Deactivate the exit hole
		exitHole.isActive = false;

		// Kill the pawn movement
		pawn.isActive = false;

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
		pawn.gameObject.SetActive(true);
		pawn.isActive = true;

		// Look at the pawn again
		currentCameraMover.cameraTarget = pawn.transform;

		// Wait a few seconds before we reactivate the exit
		yield return new WaitForSeconds(2.0f);

		// Deactivate this hole for a while
		exitHole.isActive = true;

		yield return null;
	}
}
