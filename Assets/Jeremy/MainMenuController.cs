using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public static MainMenuController _mainMenu;
	public bool mouseSceneChecker;
    public string lostScene;
    public string foundScene;

	//Stuff for dialogue
    public Image ElephantPopup;
    public Image MousePopup;
	public Image MysteryPopup;
    public float popupHeightUp;
    public float popupHeightDown;
	public Text elephantTextField;
	public Text mouseTextField;
	public Text mysteryTextField;
	public float waitTime = 2.05f;

	//Stuff for main menu
	public GameObject ElephantLogic;
	public GameObject MouseLogic;
	public Image titleMouse;
	public Color mouseTitleColor;
	public Image titleElephant;
	public Color elephantTitleColor;
	public Image creditsMouse;
	public Image creditsElephant;
	public Image fadeToWhite;

    public enum dialogueState{
		launch,
		mainMenuMouse,
		mainMenuElephant,
		mainMenuFade,
		firstDialogue,
        elephantWindow,
        elephantText,
        mouseWindow,
        mouseText,
		mysteryWindow,
		mysteryText,
		wait,
        clear
    };
    public dialogueState currentState;

	private string currentText = "";
	private string currentTextInProgress = "";
	private float countdown = 0;
	private int substate =0;
    

    // Start is called before the first frame update
    void Start()
    {
        if(_mainMenu == null)
        {
            _mainMenu = this;
            DontDestroyOnLoad(this);
        }else{
            GameObject.Destroy(this);
        }

        if(PlayerPrefs.HasKey("ElephantFound"))
        {
			mouseSceneChecker = false;
        }

		if (mouseSceneChecker == true) {
			MouseLogic.SetActive (true);
			ElephantLogic.SetActive (false);
		} else {
			MouseLogic.SetActive (false);
			ElephantLogic.SetActive (true);
		}

		//Dialogue (false, "What is going on... does this work?");
    }

    public void Update()
    {

		switch (currentState) {

		case dialogueState.launch:
			countdown += Time.deltaTime;
			fadeToWhite.color = Color.Lerp (new Color(1,1,1,1), new Color (1, 1, 1, 0), countdown/2.5f);
			if (countdown>=2.5f) {
				countdown = 0;
				fadeToWhite.color = new Color (1, 1, 1, 0);
				if (mouseSceneChecker == true) {
					currentState = dialogueState.mainMenuMouse;
				} else {
					currentState = dialogueState.mainMenuElephant;
				}
			}
			break;

		case dialogueState.mainMenuMouse:
			if (substate == 0) {
				countdown += Time.deltaTime;
				Color current = mouseTitleColor;
				current.a = 0;
				titleMouse.color = Color.Lerp (current, mouseTitleColor, countdown);
				if (countdown >= 3) {
					titleMouse.color = mouseTitleColor;
					substate = 1;
					countdown = 0;
				}
			} else if (substate == 1) {
				countdown += Time.deltaTime;
				Color current = mouseTitleColor;
				current.a = 0;
				creditsMouse.color = Color.Lerp (current, mouseTitleColor, countdown);
				if (countdown >= 3) {
					creditsMouse.color = mouseTitleColor;
					substate = 2;
					countdown = 0;
				}
			} else {
				countdown += Time.deltaTime;
				if (countdown > 3) {
					currentState = dialogueState.mainMenuFade;
					substate = 0;
					countdown = 0;
				}
			}
			break;

		case dialogueState.mainMenuElephant:
			if (substate == 0) {
				countdown += Time.deltaTime;
				Color current = elephantTitleColor;
				current.a = 0;
				titleElephant.color = Color.Lerp (current, elephantTitleColor, countdown);
				if (countdown >= 3) {
					titleElephant.color = elephantTitleColor;
					substate = 1;
					countdown = 0;
				}
			} else if (substate == 1) {
				countdown += Time.deltaTime;
				Color current = elephantTitleColor;
				current.a = 0;
				creditsElephant.color = Color.Lerp (current, elephantTitleColor, countdown);
				if (countdown >= 3) {
					creditsElephant.color = elephantTitleColor;
					substate = 2;
					countdown = 0;
				}
			} else {
				countdown += Time.deltaTime;
				if (countdown > 3) {
					currentState = dialogueState.mainMenuFade;
					substate = 0;
					countdown = 0;
				}
			}
			break;

		case dialogueState.mainMenuFade:
			bool finished = true;
			countdown += Time.deltaTime;
			if (countdown <= 2) {
				Color target = new Color (titleElephant.color.r, titleElephant.color.g, titleElephant.color.b, 0);
				titleElephant.color = Color.Lerp (titleElephant.color, target, countdown/2);
				target = new Color (titleMouse.color.r, titleMouse.color.g, titleMouse.color.b, 0);
				titleMouse.color = Color.Lerp (titleMouse.color, target,countdown/2);
				target = new Color (creditsElephant.color.r, creditsElephant.color.g, creditsElephant.color.b, 0);
				creditsElephant.color = Color.Lerp (creditsElephant.color, target, countdown/2);
				target = new Color (creditsMouse.color.r, creditsMouse.color.g, creditsMouse.color.b, 0);
				creditsMouse.color = Color.Lerp (creditsMouse.color, target, countdown/2);
			} else {
				countdown = 0;
				currentState = dialogueState.firstDialogue;
			}
			break;

		case dialogueState.elephantWindow:
			countdown += Time.deltaTime;
			float newHeight1 = Mathf.SmoothStep (popupHeightDown, popupHeightUp, countdown);
			ElephantPopup.rectTransform.anchoredPosition = new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, newHeight1);
			if (countdown>1) {
				currentState = dialogueState.elephantText;
				countdown = 0;
			}
			break;

		case dialogueState.mouseWindow:
			countdown += Time.deltaTime;
			float newHeight2 = Mathf.SmoothStep(popupHeightDown, popupHeightUp, countdown);
			MousePopup.rectTransform.anchoredPosition = new Vector2 (MousePopup.rectTransform.anchoredPosition.x, newHeight2);
			if (countdown>1) {
				countdown = 0;
				currentState = dialogueState.mouseText;
			}
			break;

		case dialogueState.mysteryWindow:
			countdown += Time.deltaTime;
			float newHeight3 = Mathf.SmoothStep(popupHeightDown, popupHeightUp, countdown);
			MysteryPopup.rectTransform.anchoredPosition = new Vector2 (MysteryPopup.rectTransform.anchoredPosition.x, newHeight3);
			if (countdown>1) {
				countdown = 0;
				currentState = dialogueState.mysteryText;
			}
			break;

		case dialogueState.elephantText:
			if (currentTextInProgress.Length < currentText.Length) {
				countdown += Time.deltaTime;
				if (countdown > 0.06f) {
					currentTextInProgress = currentText.Substring (0, currentTextInProgress.Length + 1);
					countdown = 0;
				}
			} else {
				currentTextInProgress = currentText;
				currentState = dialogueState.wait;
			}
			elephantTextField.text = currentTextInProgress;
			break;

		case dialogueState.mouseText:
			if (currentTextInProgress.Length < currentText.Length) {
				countdown += Time.deltaTime;
				if (countdown > 0.06f) {
					currentTextInProgress = currentText.Substring (0, currentTextInProgress.Length + 1);
					countdown = 0;
				}
			} else {
				currentTextInProgress = currentText;
				currentState = dialogueState.wait;
			}
			mouseTextField.text = currentTextInProgress;
			break;

		case dialogueState.mysteryText:
			if (currentTextInProgress.Length < currentText.Length) {
				countdown += Time.deltaTime;
				if (countdown > 0.06f) {
					currentTextInProgress = currentText.Substring (0, currentTextInProgress.Length + 1);
					countdown = 0;
				}
			} else {
				currentTextInProgress = currentText;
				currentState = dialogueState.wait;
			}
			mysteryTextField.text = currentTextInProgress;
			break;

		case dialogueState.firstDialogue:
			string first = "I haven’t seen Tiny since yesterday.\nHe gets nervous when he is alone.\nI should check on him!";
			Dialogue(MouseHole.DialogueTree.speaker.mouse, first);
			break;

		case dialogueState.clear:
			countdown += Time.deltaTime;
			bool fullyClear = true;
			float newHeight = 0;
			if (countdown < 1 && elephantTextField.text!="") {
				ElephantPopup.rectTransform.anchoredPosition = new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, Mathf.SmoothStep (popupHeightUp, popupHeightDown, countdown));
				fullyClear = false;
			}
			if (countdown < 1 && mouseTextField.text!="") {
				MousePopup.rectTransform.anchoredPosition = new Vector2 (MousePopup.rectTransform.anchoredPosition.x, Mathf.SmoothStep (popupHeightUp, popupHeightDown, countdown));
				fullyClear = false;
			}
			if (countdown < 1 && mysteryTextField.text!="") {
				MysteryPopup.rectTransform.anchoredPosition = new Vector2 (MysteryPopup.rectTransform.anchoredPosition.x, Mathf.SmoothStep (popupHeightUp, popupHeightDown, countdown));
				fullyClear = false;
			}
			if (fullyClear == true) {
				countdown = 0;
				elephantTextField.text = "";
				mouseTextField.text = "";
				mysteryTextField.text = "";
				currentText = "";
				currentTextInProgress = "";
			}
			break;

		case dialogueState.wait:
			countdown += Time.deltaTime;
			if (countdown >= waitTime) {
				countdown = 0;
				currentState = dialogueState.clear;
			}
			break;
		}
    }

	public void Dialogue(MouseHole.DialogueTree.speaker speaker, string text)
	{
		currentText = text;
		if (speaker == MouseHole.DialogueTree.speaker.mouse) {
			currentState = dialogueState.mouseWindow;
		} else if (speaker == MouseHole.DialogueTree.speaker.elephant){
			currentState = dialogueState.elephantWindow;
		} else if (speaker == MouseHole.DialogueTree.speaker.mystery){
			currentState = dialogueState.mysteryWindow;
		}
	}
}
