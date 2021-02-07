using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedEasterEgg : MonoBehaviour
{
	bool triggeredOnce;
	public bool tent;


	private void OnTriggerEnter(Collider other)
	{
		if (triggeredOnce == false) {
			Pawn pawn = other.GetComponent<MousePawn> ();
			// If we are a mouse
			if (pawn != null) {
				if (tent == false) {
					triggeredOnce = true;
					MainMenuController._mainMenu.Dialogue (MouseHole.DialogueTree.speaker.mouse, "This is no time for sleep!\nI've gotta find Tiny!");
				} else {
					triggeredOnce = true;
					MainMenuController._mainMenu.Dialogue (MouseHole.DialogueTree.speaker.mouse, "Whoa! This journey is intense!\n        ...Get it?\n     IN TENTS?");
				}
			}
		}
	}
}
