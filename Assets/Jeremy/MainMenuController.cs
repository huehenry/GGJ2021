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
    public float popupHeightUp;
    public float popupHeightDown;
	public Text elephantTextField;
	public Text mouseTextField;
	public float waitTime = 2;

	//Stuff for main menu
	public Camera ElephantCam;
	public Camera MouseCam;
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
        elephantWindow,
        elephantText,
        mouseWindow,
        mouseText,
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
			MouseCam.gameObject.SetActive (true);
			ElephantCam.gameObject.SetActive (false);
		} else {
			ElephantCam.gameObject.SetActive (true);
			MouseCam.gameObject.SetActive (false);
		}

		//Dialogue (false, "What is going on... does this work?");
    }

    public void Update()
    {

		switch (currentState) {

		case dialogueState.launch:
			fadeToWhite.color = Color.Lerp (fadeToWhite.color, new Color (1, 1, 1, 0), 0.01f);
			if (Mathf.Abs (fadeToWhite.color.a - 0) < 0.01f) {
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
				titleMouse.color = Color.Lerp (titleMouse.color, mouseTitleColor, 0.01f);
				if (Mathf.Abs (titleMouse.color.a - mouseTitleColor.a) < 0.01f) {
					titleMouse.color = mouseTitleColor;
					substate = 1;
				}
			} else {
				creditsMouse.color = Color.Lerp (creditsMouse.color, mouseTitleColor, 0.01f);
				if (Mathf.Abs (creditsMouse.color.a - mouseTitleColor.a) < 0.01f) {
					countdown += Time.deltaTime;
					creditsMouse.color = mouseTitleColor;
					if (countdown > 2) {
						currentState = dialogueState.mainMenuFade;
						substate = 0;
						countdown = 0;
					}
				}
			}
			break;

		case dialogueState.mainMenuElephant:
			if (substate == 0) {
				titleElephant.color = Color.Lerp (titleElephant.color, elephantTitleColor, 0.01f);
				if (Mathf.Abs (titleElephant.color.a - elephantTitleColor.a) < 0.01f) {
					titleElephant.color = elephantTitleColor;
					substate = 1;
				}
			} else {
				creditsElephant.color = Color.Lerp (creditsElephant.color, elephantTitleColor, 0.01f);
				if (Mathf.Abs (creditsElephant.color.a - elephantTitleColor.a) < 0.01f) {
					creditsElephant.color = elephantTitleColor;
					countdown += Time.deltaTime;
					if (countdown > 2) {
						currentState = dialogueState.mainMenuFade;
						substate = 0;
						countdown = 0;
					}
				}
			}
			break;

		case dialogueState.mainMenuFade:
			fadeToWhite.color = Color.Lerp (fadeToWhite.color, Color.white, 0.01f);
			if (Mathf.Abs (fadeToWhite.color.a - 1.0f) < 0.01f) {
				//DO THE LOGIC TO START THE GAME HERE.
				currentState = dialogueState.clear;
			}
			break;

		case dialogueState.elephantWindow:
			ElephantPopup.rectTransform.anchoredPosition = Vector2.Lerp (new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, ElephantPopup.rectTransform.anchoredPosition.y), new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, popupHeightUp), 0.01f);
			if (Mathf.Abs (ElephantPopup.rectTransform.anchoredPosition.y - popupHeightUp) <= 1) {
				ElephantPopup.rectTransform.anchoredPosition = new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, popupHeightUp);
				currentState = dialogueState.elephantText;
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

		case dialogueState.mouseWindow:
			MousePopup.rectTransform.anchoredPosition = Vector2.Lerp (new Vector2 (MousePopup.rectTransform.anchoredPosition.x, MousePopup.rectTransform.anchoredPosition.y), new Vector2 (MousePopup.rectTransform.anchoredPosition.x, popupHeightUp), 0.01f);
			if (Mathf.Abs (MousePopup.rectTransform.anchoredPosition.y - popupHeightUp) <= 1) {
				MousePopup.rectTransform.anchoredPosition = new Vector2 (MousePopup.rectTransform.anchoredPosition.x, popupHeightUp);
				currentState = dialogueState.mouseText;
			}
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

		case dialogueState.clear:
			ElephantPopup.rectTransform.anchoredPosition = Vector2.Lerp (new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, ElephantPopup.rectTransform.anchoredPosition.y), new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, popupHeightDown), 0.01f);
			if (Mathf.Abs (ElephantPopup.rectTransform.anchoredPosition.y - popupHeightDown) <= 1) {
				ElephantPopup.rectTransform.anchoredPosition = new Vector2 (ElephantPopup.rectTransform.anchoredPosition.x, popupHeightDown);
				elephantTextField.text = "";
			}
			MousePopup.rectTransform.anchoredPosition = Vector2.Lerp (new Vector2 (MousePopup.rectTransform.anchoredPosition.x, MousePopup.rectTransform.anchoredPosition.y), new Vector2 (MousePopup.rectTransform.anchoredPosition.x, popupHeightDown), 0.01f);
			if (Mathf.Abs (MousePopup.rectTransform.anchoredPosition.y - popupHeightDown) <= 1) {
				MousePopup.rectTransform.anchoredPosition = new Vector2 (MousePopup.rectTransform.anchoredPosition.x, popupHeightDown);
				mouseTextField.text = "";
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

	public void Dialogue(bool mouse, string text)
	{
		currentText = text;
		if (mouse) {
			currentState = dialogueState.mouseWindow;
		} else {
			currentState = dialogueState.elephantWindow;
		}
	}
}
