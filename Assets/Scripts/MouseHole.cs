using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHole : MonoBehaviour
{
    public MouseHole exitHole;
    public bool isActive;

	[System.Serializable]
	public class DialogueTree
	{
		[TextArea]
		public string dialogue;
		public speaker whoSaysThis;
		public float timeTheySayItAt;
		public enum speaker{
			mouse,
			elephant,
			mystery
		}
		public bool triggered;
	}
	public DialogueTree[] dialogue;
	private float dialogueTimer;
	public bool triggerDialogue;

	public float addedDelay;

	public bool turnRainOn = false;
	public bool turnRainOff = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//This hole's dialogue has triggered, meaning it was an exit hole, meaning the player teleported here
		if (triggerDialogue == true) {
			//Increment timer so we can time dialogue.
			dialogueTimer += Time.deltaTime;
			//Loop through the dialogue in this hole
			foreach (DialogueTree d in dialogue) {
				//Only run this logic if the trigger hasn't happened once before
				if (d.triggered == false) {
					//Check if it's time.
					if (d.timeTheySayItAt <= dialogueTimer) {
						//Pop up this dialogue with the string.
						MainMenuController._mainMenu.Dialogue(d.whoSaysThis, d.dialogue);
						//Trigger this so each dialogue is only used once
						d.triggered = true;
					}
				}
			}
		}
    }

    private IEnumerator TeleportCoroutine(Pawn pawn)
    {
        // Deactivate this hole for a while
        isActive = false;

        // Set our checkpoint to the exit hole
        GameManager.instance.lastMouseCheckpoint = exitHole;

        // Deactivate the exit hole
        exitHole.isActive = false;

        // Kill the pawn movement
        pawn.isActive = false;

        // Set the camera to look at this hole, not the mouse!
        CameraMover currentCameraMover = GameManager.instance.currentCamera.GetComponent<CameraMover>();
        currentCameraMover.cameraTarget = transform;

        //TODO: Start particle effect and sound
        Instantiate(GameManager.instance.entryParticle, transform.position, transform.rotation);

        if (GameManager.instance.enterHoleSound != null) {
            AudioSource.PlayClipAtPoint(GameManager.instance.enterHoleSound, transform.position);
        }

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

        // Wait some amount of time... whatever
		yield return new WaitForSeconds(GameManager.instance.delayMove + addedDelay);

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

        // Reactivate this hole 
        isActive = true;

		//START the dialogue for the exit.
		exitHole.triggerDialogue = true;

		yield return new WaitForSeconds(1.0f);

		//If this is a rainhole, turn on rain.
		if (turnRainOn == true) {
			MainMenuController._mainMenu.rainOnMe = true;
		} else if (turnRainOff == true) {
			MainMenuController._mainMenu.rainOnMe = false;
		}


        // Wait a few seconds before we reactivate the exit
        yield return new WaitForSeconds(2.0f);

        // Deactivate this hole for a while
        exitHole.isActive = true;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we have an exit and we are not deactivated
        if (exitHole != null && isActive) {
            // Get the pawn (so we can pass it in to the coroutine)
            Pawn pawn = other.GetComponent<MousePawn>();
            // If we are a mouse
            if (pawn != null) {
                // Start Teleport Coroutine
                StartCoroutine(TeleportCoroutine(pawn));
            }
        }
    }


}
