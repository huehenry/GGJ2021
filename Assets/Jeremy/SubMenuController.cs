using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SubMenuController : MonoBehaviour
{

    public string sceneToStart;

    public void PushedButtonToStart()
    {
        SceneManager.LoadScene(sceneToStart);
    }
}
