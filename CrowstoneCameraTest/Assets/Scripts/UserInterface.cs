//created by Jared Shaw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// handles the user interface such as scene name
/// and pause screen
/// </summary>
public class UserInterface : MonoBehaviour {

    public float displayTime = 2.0f;
    public float textFadeSpeed = 0.8f;
    public GameObject gameUI;
    public GameObject pauseScreen;
    public Text sceneNameText;
    public float pauseScreenFadeSpeed = 0.8f;

    private bool fadingOutInProcess;
    private bool fadingInInProcess;
    private float pausePanelAlpha;
    private GameObject player;
    private bool onPauseScreen;
    private bool cursorActive;
    

	// Use this for initialization
	void Start () {
        //disable parent object called gameUI
		if(gameUI != null)
        {
            gameUI.gameObject.SetActive(false);
        }
        //disable pause screen
        if(pauseScreen != null)
        {
            pauseScreen.gameObject.SetActive(false);
            pausePanelAlpha = pauseScreen.GetComponentInChildren<Image>().color.a;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        cursorActive = false;
    }

    /// <summary>
    /// add delegates to events
    /// </summary>
    private void OnEnable()
    {
        LevelManager.onFadeInFinished += startDisplaySceneName;
        InputManager.onPausePressed += pausePressed;
    }

    /// <summary>
    /// remove delegates from events
    /// </summary>
    private void OnDisable()
    {
        LevelManager.onFadeInFinished -= startDisplaySceneName;
        InputManager.onPausePressed -= pausePressed;
    }
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// is called when fade in has finished after loading a scene
    /// </summary>
    /// <param name="name"></param>
    private void startDisplaySceneName(string name)
    {
        StartCoroutine(displaySceneName(name));
    }

    /// <summary>
    /// controls the behaviour of the mouse when pause screen
    /// has been enable and disabled
    /// </summary>
    private void OnGUI()
    {
        if (onPauseScreen && !cursorActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cursorActive = true;
        }

        if(!onPauseScreen && cursorActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cursorActive = false;
        }
    }

    /// <summary>
    /// is called from event in input manager when ESC or P is pressed
    /// </summary>
    private void pausePressed()
    {
        //if nothing is currently being faded in or out
        if(!fadingOutInProcess && !fadingInInProcess)
        {
            //if the pause screen is currently enabled, 
            // then fade it out
            if (pauseScreen.gameObject.activeSelf)
            {
                fadingOutInProcess = true;
                StartCoroutine(undisplayPauseScreen());
                SoundManager.Instance.playPauseOnOff();
                GameManager.gameManager.SetGlow(true);
            }
            //if the pause screen is disabled then
            //start fading it in
            else
            {
                fadingInInProcess = true;
                StartCoroutine(displayPauseScreen());
                SoundManager.Instance.playPauseOnOff();
                GameManager.gameManager.SetGlow(false);
            }
        }
    }

    /// <summary>
    /// is called when player clicks "Resume" on the pause screen
    /// </summary>
    public void PauseScreenResumeClicked()
    {
        //if nothing is being faded in or out
        //then fade out the pause screen
        if(!fadingOutInProcess && !fadingInInProcess)
        {
            SoundManager.Instance.playMenuClick();

            fadingOutInProcess = true;
            StartCoroutine(undisplayPauseScreen());
            GameManager.gameManager.SetGlow(true);
        }
    }

    /// <summary>
    /// is called when player clicks "Main menu" on the pause screen
    /// </summary>
    public void PauseScreenMainMenuClicked()
    {
        //if nothing is being faded in or out
        //then reset game state and load the main menu
        if (!fadingOutInProcess && !fadingInInProcess)
        {
            SoundManager.Instance.playMenuClick();


            fadingOutInProcess = true;
            LevelManager.Instance.startLoadSpecificScene("Start Menu");
            GameManager.gameManager.resetGameState();
        }
    }

    /// <summary>
    /// if player clicks "quit" on the pause screen, quits the game
    /// </summary>
    public void PauseScreenQuit()
    {
        SoundManager.Instance.playMenuClick();

        Application.Quit();
    }

    /// <summary>
    /// is run as a co-routine
    /// fades in the pause screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator displayPauseScreen()
    {
        onPauseScreen = true;
        GameManager.gameManager.flipPause();

        //fade in screen
        pauseScreen.gameObject.SetActive(true);
        fadingInInProcess = true;
        Image background = pauseScreen.GetComponentInChildren<Image>();
        Text[] texts = pauseScreen.GetComponentsInChildren<Text>();

        StartCoroutine(FadeInImage(background));
        foreach(Text txt in texts)
        {
            StartCoroutine(FadeInText(txt, pauseScreenFadeSpeed));
        }

        //stop movement of player
        player.GetComponentInChildren<FirstPersonController>().enabled = false;
        //player.GetComponentInChildren<MouseLook>().enabled = false;
        
        yield return null;
    }

    /// <summary>
    /// is run as a co-routine
    /// fades out the pause screen
    /// </summary>
    /// <returns></returns>
    private IEnumerator undisplayPauseScreen()
    {
        //fade out screen
        fadingOutInProcess = true;
        Image background = pauseScreen.GetComponentInChildren<Image>();
        Text[] texts = pauseScreen.GetComponentsInChildren<Text>();

        StartCoroutine(FadeOutImage(background));
        foreach (Text txt in texts)
        {
            StartCoroutine(FadeOutText(txt, pauseScreenFadeSpeed));
        }

        //deactivate panel
        StartCoroutine(DeactivatePanel(pauseScreen));

        if (GameManager.gameManager.getInUI() == false) {
            //restart movement of player and camera
            player.GetComponentInChildren<FirstPersonController>().enabled = true;
            //player.GetComponentInChildren<MouseLook>().enabled = true;
        }

        onPauseScreen = false;
        GameManager.gameManager.flipPause();

        yield return null;
    }

    /// <summary>
    /// displays the scene name in the top left corner
    /// is called after fadein event is done
    /// </summary>
    /// <param name="name">string</param>
    /// <returns></returns>
    private IEnumerator displaySceneName(string name)
    {
        /*
        //set text
        gameUI.gameObject.SetActive(true);
        sceneNameText.text = name;

        //fade in text
        sceneNameText.color = new Color(sceneNameText.color.r, sceneNameText.color.g, sceneNameText.color.b, 0);
        StartCoroutine(FadeInText(sceneNameText, textFadeSpeed));

        while (fadingInInProcess)
        {
            yield return null;
        }

        //wait given time
        yield return new WaitForSeconds(displayTime);

        //fade out text
        StartCoroutine(FadeOutText(sceneNameText, textFadeSpeed));
        gameUI.gameObject.SetActive(true);
        */    
        yield return null;

    }

    /// <summary>
    /// changes the alpha value of text over time with the given speed
    /// from what its current value to 0
    /// </summary>
    /// <param name="text"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    IEnumerator FadeOutText(Text text, float speed)
    {
        fadingOutInProcess = true;

        float alpha = text.color.a;
        while (text.color.a > 0)
        {
            alpha -= Time.deltaTime * speed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingOutInProcess = false;

        yield return null;
    }


    /// <summary>
    /// changes the alpha value of the given text over time
    /// from 0 to 1
    /// </summary>
    /// <param name="text"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    IEnumerator FadeInText(Text text, float speed)
    {
        fadingInInProcess = true;

        float alpha = 0.0f;
        while (alpha < 1)
        {

            alpha += Time.deltaTime * speed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;
        yield return null;
    }

    /// <summary>
    /// changes the current alpha value of the given image over time
    /// to zero
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeOutImage(Image image)
    {
        fadingOutInProcess = true;

        float alpha = image.color.a;
        while (image.color.a > 0)
        {
            alpha -= Time.deltaTime * pauseScreenFadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        fadingOutInProcess = false;

        yield return null;
    }

    /// <summary>
    /// changes the alpha value of the given image over time
    /// from 0 to 1
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeInImage(Image image)
    {
        fadingInInProcess = true;

        float alpha = 0.0f;
        while (alpha < pausePanelAlpha)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            alpha += Time.deltaTime * pauseScreenFadeSpeed;

            yield return null;
        }

        fadingInInProcess = false;

        yield return null;
    }

    /// <summary>
    /// gives enough time for panel to fade out
    /// then deactivates it
    /// </summary>
    /// <param name="panel"></param>
    /// <returns></returns>
    IEnumerator DeactivatePanel(GameObject panel)
    {
        yield return new WaitForSeconds(1.0f / pauseScreenFadeSpeed);

        panel.gameObject.SetActive(false);
    }

    /// <summary>
    /// returns on pause screen boolean
    /// </summary>
    /// <returns>bool</returns>
    public bool OnPauseScreen()
    {
        return onPauseScreen;
    }
}
