using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantWinCondition : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        // If we are a mouse
        ElephantPawn pawn = other.GetComponent<ElephantPawn>();
        if (pawn != null) {

            // Stop our controls
            pawn.isActive = false;
            pawn.moveVector = Vector3.zero;
            pawn.rb.velocity = Vector3.zero;

            //TODO: Force elephant to look the right way?
            pawn.cameraShake = false;
            pawn.trail.Stop();

            // Play trumpet sound and animation
            AudioSource.PlayClipAtPoint(GameManager.instance.trumpetSound, transform.position);
            pawn.anim.SetTrigger("Trumpet");

            //TODO: Start up win dialogue

            // Save player prefs
            PlayerPrefs.SetInt("ElephantFound", 0);
			MainMenuController._mainMenu.TriggerTheEnd (false);

            //TODO: open Exit Game button screen???          
        }
    }

}
