using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This scripts controls transitions on the main menu
/// after a button has been clicked, for the purpose of
/// adding fading
/// </summary>
public class MenuController : MonoBehaviour {

    public GameObject title;
    public GameObject PressAnyKeyPanel;
    public GameObject MenuPanel;
    public GameObject ControlsPanel;
    public float fadeSpeed = 1.0f;

    private bool fadingOutInProcess;
    private bool fadingInInProcess;
    private bool onTitleScreen; 

	// Use this for initialization
	void Start () {
        onTitleScreen = true;
        title.gameObject.SetActive(true);
        PressAnyKeyPanel.gameObject.SetActive(true);
        MenuPanel.gameObject.SetActive(false);
        ControlsPanel.gameObject.SetActive(false);

        fadingOutInProcess = false;
        fadingInInProcess = false;
	}
	
	// Update is called once per frame
	void Update () {
        // handles the "press any key" event from the initial start screen
        if (onTitleScreen)
        {
            if (Input.anyKey)
            {
                FadeToMenu();
                onTitleScreen = false;
            }
        }
    }

    /// <summary>
    /// handles the transition from "press any key" screen
    /// to the main menu
    /// </summary>
    public void FadeToMenu()
    {
        //if we are not already fading someting in or out
        if (!fadingInInProcess && !fadingOutInProcess)
        {
            //sound fx
            SoundManager.Instance.playMenuClick();

            //fade out pressanykey screen
            Text pressKeyText = PressAnyKeyPanel.GetComponentInChildren<Text>();
            StartCoroutine(FadeOutText(pressKeyText));
            StartCoroutine(DeactivatePanel(PressAnyKeyPanel));

            //fade in menu
            MenuPanel.gameObject.SetActive(true);

            Text[] texts = MenuPanel.GetComponentsInChildren<Text>();

            foreach (Text txt in texts)
            {
                if (txt.name.Equals("Continue"))
                {
                    txt.GetComponent<Button>().interactable = false;
                }
                StartCoroutine(FadeInText(txt));
            }
        }
    }

    /// <summary>
    /// handles the transition from the main menu to starting the game
    /// </summary>
    public void MainMenuStart()
    {
        if(!fadingInInProcess && !fadingOutInProcess)
        {
            //sound fx
            SoundManager.Instance.playMenuStartClicked();

            //change menu fade speed to that of loading screen
            fadeSpeed = LevelManager.Instance.getLoadingFadeSpeed();

            //fade out menu and title
            Text titleText = title.GetComponent<Text>();
            StartCoroutine(FadeOutText(titleText));

            Text[] menuTexts = MenuPanel.GetComponentsInChildren<Text>();
            foreach (Text txt in menuTexts)
            {
                StartCoroutine(FadeOutText(txt));
            }

            //set current day and start loading the scene
            GameManager.gameManager.setCurrentDay(1);
            LevelManager.Instance.startLoadSpecificScene("Town");
        }
    }

    /// <summary>
    /// handles the transition from main menu->continue
    /// no save game functionality yet
    /// </summary>
    public void MainMenuContinue()
    {
        //sound fx
        SoundManager.Instance.playMenuClick();
        Debug.Log("Continue clicked");
    }

    public void MainMenuControlsClicked()
    {
        SoundManager.Instance.playMenuClick();

        if (!fadingInInProcess && !fadingOutInProcess)
        {
            //fade out main menu screen
            Text[] menuText = MenuPanel.GetComponentsInChildren<Text>();

            foreach(Text text in menuText)
            {
                StartCoroutine(FadeOutText(text));
            }
            StartCoroutine(DeactivatePanel(MenuPanel));

            //fade in controls
            ControlsPanel.gameObject.SetActive(true);
            Text[] controlsText = ControlsPanel.GetComponentsInChildren<Text>();
            
            foreach(Text text in controlsText)
            {
                StartCoroutine(FadeInText(text));
            }
        }
    }

    public void MainMenuControlsBackClicked()
    {
        SoundManager.Instance.playMenuClick();

        if (!fadingInInProcess && !fadingOutInProcess)
        {
            Text[] controlsText = ControlsPanel.GetComponentsInChildren<Text>();

            foreach (Text text in controlsText)
            {
                StartCoroutine(FadeOutText(text));
            }
            StartCoroutine(DeactivatePanel(ControlsPanel));

            //fade in menu
            MenuPanel.gameObject.SetActive(true);

            Text[] texts = MenuPanel.GetComponentsInChildren<Text>();

            foreach (Text txt in texts)
            {
                if (txt.name.Equals("Continue"))
                {
                    txt.GetComponent<Button>().interactable = false;
                }
                StartCoroutine(FadeInText(txt));
            }

        }
    }

    /// <summary>
    /// main menu->quit game
    /// </summary>
    public void MainMenuQuit()
    {
        if(!fadingInInProcess && !fadingOutInProcess)
        {
            Application.Quit();

        }
    }

    /// <summary>
    /// changes the alpha value over time on the font of the given text from
    /// whatever value it was until it reaches 0
    /// </summary>
    /// <param name="text">Text</param>
    /// <returns></returns>
    IEnumerator FadeOutText(Text text)
    {
        fadingOutInProcess = true;

        float alpha = text.color.a;
        while (text.color.a > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingOutInProcess = false;

        yield return null;
    }

    /// <summary>
    /// changes the alpha value over time of the given text from 0 to 1
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator FadeInText(Text text)
    {
        fadingInInProcess = true;

        float alpha = 0.0f;
        while (alpha < 1)
        {

            alpha += Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;
        yield return null;
    }

    /// <summary>
    /// changes the alpha value of the given image over time
    /// from whatever it was until it reaches 0
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeOutImage(Image image)
    {
        fadingOutInProcess = true;

        float alpha = image.color.a;
        while (image.color.a > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
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
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }

        fadingInInProcess = false;

        yield return null;
    }

    /// <summary>
    /// is called after a fade out is called.
    /// after allowing for enough time for the panel to fade 
    /// out it then deactivates it
    /// </summary>
    /// <param name="panel"></param>
    /// <returns></returns>
    IEnumerator DeactivatePanel(GameObject panel)
    {
        yield return new WaitForSeconds(1.0f / fadeSpeed);

        panel.gameObject.SetActive(false);
    }
}
