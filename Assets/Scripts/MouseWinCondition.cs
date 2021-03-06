using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWinCondition : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        // If we are a mouse
        MousePawn pawn = other.GetComponent<MousePawn>();
        if (pawn != null) {

            // Stop our controls
            pawn.isActive = false;
            pawn.moveVector = Vector3.zero;
            pawn.rb.velocity = Vector3.zero;

            // Force mouse to look forward
            pawn.transform.rotation = Quaternion.identity;
            pawn.cameraShake = false;
            pawn.trail.Stop();

            // Play trumpet sound
            AudioSource.PlayClipAtPoint(GameManager.instance.trumpetSound, transform.position);

            //TODO: Start up win dialogue

            // Save player prefs
            PlayerPrefs.SetInt("ElephantFound", 1);
			MainMenuController._mainMenu.TriggerTheEnd (true);

            //TODO: open Exit Game button screen???          
        }
    }

}
