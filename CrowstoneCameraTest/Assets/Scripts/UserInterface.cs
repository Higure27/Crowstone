using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		if(gameUI != null)
        {
            gameUI.gameObject.SetActive(false);
        }
        if(pauseScreen != null)
        {
            pauseScreen.gameObject.SetActive(false);
            pausePanelAlpha = pauseScreen.GetComponentInChildren<Image>().color.a;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        cursorActive = false;
    }

    private void OnEnable()
    {
        LevelManager.onFadeInFinished += startDisplaySceneName;
        InputManager.onPausePressed += pausePressed;
    }

    private void OnDisable()
    {
        LevelManager.onFadeInFinished -= startDisplaySceneName;
        InputManager.onPausePressed -= pausePressed;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void startDisplaySceneName(string name)
    {
        StartCoroutine(displaySceneName(name));
    }

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

    private void pausePressed()
    {
        if(!fadingOutInProcess && !fadingInInProcess)
        {
            if (pauseScreen.gameObject.activeSelf)
            {
                fadingOutInProcess = true;
                StartCoroutine(undisplayPauseScreen());
            }
            else
            {
                fadingInInProcess = true;
                StartCoroutine(displayPauseScreen());
            }
        }
    }

    public void PauseScreenResumeClicked()
    {

        if(!fadingOutInProcess && !fadingInInProcess)
        {
            fadingOutInProcess = true;
            StartCoroutine(undisplayPauseScreen());
        }
    }

    public void PauseScreenMainMenuClicked()
    {
        if (!fadingOutInProcess && !fadingInInProcess)
        {
            fadingOutInProcess = true;
            LevelManager.Instance.startLoadSpecificScene("Start Menu");
        }
    }

    public void PauseScreenQuit()
    {
        Debug.Log("got here");
        Application.Quit();
    }

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

        //stop movement of player and camera

        player.GetComponentInChildren<FirstPersonController>().enabled = false;
        //player.GetComponentInChildren<MouseLook>().enabled = false;
        

        yield return null;
    }

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

    IEnumerator DeactivatePanel(GameObject panel)
    {
        yield return new WaitForSeconds(1.0f / pauseScreenFadeSpeed);

        panel.gameObject.SetActive(false);
    }

    public bool OnPauseScreen()
    {
        return onPauseScreen;
    }
}
