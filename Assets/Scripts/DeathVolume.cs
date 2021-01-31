using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolume : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        MousePawn pawn = other.GetComponent<MousePawn>();
        if (pawn != null) {
            StartCoroutine(DoHoleDeath(pawn));
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
}
