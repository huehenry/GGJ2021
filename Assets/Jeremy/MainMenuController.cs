using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public static MainMenuController _mainMenu;
    public string lostScene;
    public string foundScene;

    public Image ElephantPopup;
    public Image MousePopup;
    public float popupHeightUp;
    public float popupHeightDown;
	public Text elephantTextField;
	public Text mouseTextField;
	public float waitTime = 2;

    public enum dialogueState{
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

        /*if(PlayerPrefs.HasKey("Found"))
        {
            SceneManager.LoadScene(foundScene);
        }else{
            SceneManager.LoadScene(lostScene);
        }*/

		Dialogue (true, "What is going on... does this work");
    }

    public void Update()
    {

		switch (currentState) {
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
				if (countdown > 0.1) {
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
				if (countdown > 0.1) {
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
