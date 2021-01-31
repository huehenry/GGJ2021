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
	public MusicAudioHandler audio;

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
	public Image controls;

	//Stuff for ending.
	public MouseHole proxyEndingMouse;
	public MouseHole proxyEndingElephant;
	public Button QuitButtonMover;

	//Used for freezing animals
	public Pawn elephantPawn;
	public Pawn mousePawn;

    public enum dialogueState{
		mainMenu,
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

	public enum mainMenuState{
		launch,
		mainMenuMouse,
		mainMenuElephant,
		mainMenuFade,
		controls,
		mainMenuOver,
		endGameMouse,
		endGameElephant,
		exitButton
	}

	public mainMenuState gameStartState;
    public dialogueState currentState;

	private string currentText = "";
	private string currentTextInProgress = "";
	private float countdown = 0;
	private float countDownMenu = 0;
	private int menuSubstate = 0;
	private int substate =0;

	//HACK FOR PATICLES
	public bool gameStart = false;
    

    // Start is called before the first frame update
    void Start()
    {

		elephantPawn.isActive = false;
		mousePawn.isActive = false;
        if(_mainMenu == null)
        {
            _mainMenu = this;
            DontDestroyOnLoad(this);
        }else{
            GameObject.Destroy(this);
        }

        if(PlayerPrefs.HasKey("ElephantFound"))
        {
            if (PlayerPrefs.GetInt("ElephantFound") == 1) { mouseSceneChecker = false; }
            else { mouseSceneChecker = true;  }
        }

		if (mouseSceneChecker == true) {
			MouseLogic.SetActive (true);
			ElephantLogic.SetActive (false);
			audio.PlayMusic (true);
		} else {
			MouseLogic.SetActive (false);
			ElephantLogic.SetActive (true);
			audio.PlayMusic (false);
		}
			
    }

    public void Update()
    {

		switch (gameStartState) {

		case mainMenuState.launch:
			countDownMenu += Time.deltaTime;
			fadeToWhite.color = Color.Lerp (new Color(1,1,1,1), new Color (1, 1, 1, 0), countDownMenu/2.5f);
			if (countDownMenu>=2.5f) {
				countDownMenu = 0;
				fadeToWhite.color = new Color (1, 1, 1, 0);
				if (mouseSceneChecker == true) {
					gameStartState = mainMenuState.mainMenuMouse;
				} else {
					gameStartState = mainMenuState.mainMenuElephant;
				}
			}
			break;

		case mainMenuState.mainMenuMouse:
			if (menuSubstate == 0) {
				countDownMenu += Time.deltaTime;
				Color current = mouseTitleColor;
				current.a = 0;
				titleMouse.color = Color.Lerp (current, mouseTitleColor, countDownMenu);
				if (countDownMenu >= 3) {
					titleMouse.color = mouseTitleColor;
					menuSubstate = 1;
					countDownMenu = 0;
				}
			} else if (menuSubstate == 1) {
				countDownMenu += Time.deltaTime;
				Color current = mouseTitleColor;
				current.a = 0;
				creditsMouse.color = Color.Lerp (current, mouseTitleColor, countDownMenu);
				if (countDownMenu >= 3) {
					creditsMouse.color = mouseTitleColor;
					menuSubstate = 2;
					countDownMenu = 0;
				}
			} else {
				countDownMenu += Time.deltaTime;
				if (countDownMenu > 3) {
					gameStartState = mainMenuState.mainMenuFade;
					menuSubstate = 0;
					countDownMenu = 0;
				}
			}
			break;

		case mainMenuState.mainMenuElephant:
			if (menuSubstate == 0) {
				countDownMenu += Time.deltaTime;
				Color current = elephantTitleColor;
				current.a = 0;
				titleElephant.color = Color.Lerp (current, elephantTitleColor, countDownMenu);
				if (countDownMenu >= 3) {
					titleElephant.color = elephantTitleColor;
					menuSubstate = 1;
					countDownMenu = 0;
				}
			} else if (menuSubstate == 1) {
				countDownMenu += Time.deltaTime;
				Color current = elephantTitleColor;
				current.a = 0;
				creditsElephant.color = Color.Lerp (current, elephantTitleColor, countDownMenu);
				if (countDownMenu >= 3) {
					creditsElephant.color = elephantTitleColor;
					menuSubstate = 2;
					countDownMenu = 0;
				}
			} else {
				countDownMenu += Time.deltaTime;
				if (countDownMenu > 3) {
					gameStartState = mainMenuState.mainMenuFade;
					menuSubstate = 0;
					countDownMenu = 0;
				}
			}
			break;

		case mainMenuState.mainMenuFade:
			bool finished = true;
			countDownMenu += Time.deltaTime;
			if (countDownMenu <= 2) {
				Color target = new Color (titleElephant.color.r, titleElephant.color.g, titleElephant.color.b, 0);
				titleElephant.color = Color.Lerp (titleElephant.color, target, countDownMenu/2);
				target = new Color (titleMouse.color.r, titleMouse.color.g, titleMouse.color.b, 0);
				titleMouse.color = Color.Lerp (titleMouse.color, target,countDownMenu/2);
				target = new Color (creditsElephant.color.r, creditsElephant.color.g, creditsElephant.color.b, 0);
				creditsElephant.color = Color.Lerp (creditsElephant.color, target, countDownMenu/2);
				target = new Color (creditsMouse.color.r, creditsMouse.color.g, creditsMouse.color.b, 0);
				creditsMouse.color = Color.Lerp (creditsMouse.color, target, countDownMenu/2);
			} else {
				countDownMenu = 0;
				currentState = dialogueState.firstDialogue;
				if (mouseSceneChecker == true) {
					mousePawn.isActive = true;
					gameStart = true;
				} else {
					elephantPawn.isActive = true;
					gameStart = true;
				}
				gameStartState = mainMenuState.controls;
			}
			break;

		case mainMenuState.controls:
			countDownMenu += Time.deltaTime;
			Color controlColors = Color.white;
			controlColors.a = 0;
			if (menuSubstate == 0) {
					controls.color = Color.Lerp (controlColors, Color.white, countDownMenu - 2);
					if (countDownMenu >= 10) {
						controls.color = controlColors;
						countDownMenu = 0;
						menuSubstate += 1;
					}
			} else {
					//Fade controls
					controls.color = Color.Lerp (Color.white, new Color (1, 1, 1, 0), countDownMenu/4);
					if (countDownMenu > 5) {
						countDownMenu = 0;
						menuSubstate = 0;
						gameStartState = mainMenuState.mainMenuOver;
					}
			}

			break;



		case mainMenuState.endGameMouse:
			mousePawn.isActive = false;
			proxyEndingMouse.triggerDialogue = true;
			bool finishedMouse = true;
			foreach (MouseHole.DialogueTree d in proxyEndingMouse.dialogue) {
				if (d.triggered == false) {
					finishedMouse = false;
				}
			}
			if (finishedMouse == true) {
				//Add some time here
				countDownMenu+=Time.deltaTime;
				if(countDownMenu>5)
				{
					gameStartState = mainMenuState.exitButton;
					countDownMenu = 0;
				}
			}
			break;

		case mainMenuState.endGameElephant:
			elephantPawn.isActive = false;
			proxyEndingElephant.triggerDialogue = true;
			bool finishedElephant = true;
			foreach (MouseHole.DialogueTree d in proxyEndingElephant.dialogue) {
				if (d.triggered == false) {
					finishedElephant = false;
				}
			}
			if (finishedElephant == true) {
				//Add some time here
				countDownMenu+=Time.deltaTime;
				if(countDownMenu>5)
				{
					gameStartState = mainMenuState.exitButton;
					countDownMenu = 0;
				}
			}
			break;


		case mainMenuState.exitButton:
			countDownMenu += Time.deltaTime;
			Vector2 newPos = new Vector2(QuitButtonMover.GetComponent<RectTransform>().anchoredPosition.x, Mathf.SmoothStep(-150, 65, countDownMenu));
			QuitButtonMover.GetComponent<RectTransform>().anchoredPosition = newPos;
				break;
		}



		switch (currentState) {


		case dialogueState.elephantWindow:
			countdown += Time.deltaTime;
			float newHeight1 = Mathf.SmoothStep (popupHeightDown, popupHeightUp, countdown*2);
			ElephantPopup.rectTransform.anchoredPosition = new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, newHeight1);
			if (countdown>0.5f) {
				currentState = dialogueState.elephantText;
				countdown = 0;
			}
			break;

		case dialogueState.mouseWindow:
			countdown += Time.deltaTime;
			float newHeight2 = Mathf.SmoothStep(popupHeightDown, popupHeightUp, countdown*2);
			MousePopup.rectTransform.anchoredPosition = new Vector2 (MousePopup.rectTransform.anchoredPosition.x, newHeight2);
			if (countdown>0.5f) {
				countdown = 0;
				currentState = dialogueState.mouseText;
			}
			break;

		case dialogueState.mysteryWindow:
			countdown += Time.deltaTime;
			float newHeight3 = Mathf.SmoothStep(popupHeightDown, popupHeightUp, countdown*2);
			MysteryPopup.rectTransform.anchoredPosition = new Vector2 (MysteryPopup.rectTransform.anchoredPosition.x, newHeight3);
			if (countdown>0.5f) {
				countdown = 0;
				currentState = dialogueState.mysteryText;
			}
			break;

		case dialogueState.elephantText:
			if (currentTextInProgress.Length < currentText.Length) {
				audio.elephantTalking = true;
				countdown += Time.deltaTime;
				if (countdown > 0.0275f) {
					currentTextInProgress = currentText.Substring (0, currentTextInProgress.Length + 1);
					countdown = 0;
				}
			} else {
				currentTextInProgress = currentText;
				currentState = dialogueState.wait;
				audio.elephantTalking = false;
			}
			elephantTextField.text = currentTextInProgress;
			break;

		case dialogueState.mouseText:
			if (currentTextInProgress.Length < currentText.Length) {
				countdown += Time.deltaTime;
				audio.mouseTalking = true;
				if (countdown > 0.0275f) {
					currentTextInProgress = currentText.Substring (0, currentTextInProgress.Length + 1);
					countdown = 0;
				}
			} else {
				currentTextInProgress = currentText;
				currentState = dialogueState.wait;
				audio.mouseTalking = false;
			}
			mouseTextField.text = currentTextInProgress;
			break;

		case dialogueState.mysteryText:
			if (currentTextInProgress.Length < currentText.Length) {
				audio.elephantTalking = true;
				countdown += Time.deltaTime;
				if (countdown > 0.0275f) {
					currentTextInProgress = currentText.Substring (0, currentTextInProgress.Length + 1);
					countdown = 0;
				}
			} else {
				currentTextInProgress = currentText;
				currentState = dialogueState.wait;
				audio.elephantTalking = false;
			}
			mysteryTextField.text = currentTextInProgress;
			break;

		case dialogueState.firstDialogue:
			if (mouseSceneChecker == true) {
				string first = "I haven’t seen Tiny since yesterday.\nHe gets nervous when he is alone.\nI should check on him!";
				Dialogue (MouseHole.DialogueTree.speaker.mouse, first);
			} else {
				string first = "Hey Buddy! Now that you're here,\nlet's get LOST together!";
				Dialogue (MouseHole.DialogueTree.speaker.elephant, first);
			}
			break;

		case dialogueState.clear:
			countdown += Time.deltaTime;
			bool fullyClear = true;
			float newHeight = 0;
			if (countdown < 0.5f && elephantTextField.text!="") {
				ElephantPopup.rectTransform.anchoredPosition = new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, Mathf.SmoothStep (popupHeightUp, popupHeightDown, countdown*2));
				fullyClear = false;
			}
			if (countdown < 0.5f && mouseTextField.text!="") {
				MousePopup.rectTransform.anchoredPosition = new Vector2 (MousePopup.rectTransform.anchoredPosition.x, Mathf.SmoothStep (popupHeightUp, popupHeightDown, countdown*2));
				fullyClear = false;
			}
			if (countdown < 0.5f && mysteryTextField.text!="") {
				MysteryPopup.rectTransform.anchoredPosition = new Vector2 (MysteryPopup.rectTransform.anchoredPosition.x, Mathf.SmoothStep (popupHeightUp, popupHeightDown, countdown*2));
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

	public void TriggerTheEnd(bool mouse)
	{
		if (mouse == true) {
			gameStartState = mainMenuState.endGameMouse;
		} else {
			gameStartState = mainMenuState.endGameElephant;
		}
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
