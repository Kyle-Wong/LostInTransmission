using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    // Use this for initialization
    public enum MenuState
    {
        MainMenu, Credits
    }
    [HideInInspector]
    public MenuState menuState;
    private Canvas mainMenuCanvas;
    public Canvas creditsCanvas;
    public GameObject playButton;
    public GameObject creditsBackButton;
    public string firstLevel;
    private EventSystem eventSystem;
    private bool allowInput;
	void Start () {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        menuState = MenuState.MainMenu;
        mainMenuCanvas = GetComponent<Canvas>();
        mainMenuCanvas.enabled = true;
        creditsCanvas.enabled = false;
        allowInput = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playButtonPress()
    {
        if (allowInput)
        {
            StartCoroutine(transitionThenLoad(firstLevel, 0.25f));
        }
    }
    public void creditsButtonPress()
    {
        if (allowInput)
        {
            creditsCanvas.enabled = true;
            eventSystem.SetSelectedGameObject(creditsBackButton);
        }
    }
    public void creditsBackButtonPress()
    {
        if (allowInput)
        {
            creditsCanvas.enabled = false;
            eventSystem.SetSelectedGameObject(playButton);
        }
    }
    public void quitButtonPress()
    {
        if (allowInput)
        {
            Application.Quit();
        }
    }
    public IEnumerator transitionThenLoad(string level, float delay)
    {
        allowInput = false;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(level);
    }
}
